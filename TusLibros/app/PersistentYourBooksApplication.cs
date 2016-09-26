using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using TusLibros.clocks;
using TusLibros.db;
using TusLibros.model;
using TusLibros.model.entities;

namespace TusLibros.app
{
    internal class PersistentYourBooksApplication : IYourBooksApplication
    {
        public IClock Clock { get; set; }

        public PersistentYourBooksApplication(IClock clock)
        {
            Clock = clock;
        }

        public Cart CreateCart(Guid clientId, String password)
        {
            Cart aCart = new Cart();

            ISession session = SessionManager.OpenSession();
            ITransaction transaction = session.BeginTransaction();
            Client aClient = GetClient(clientId, password, session);
            session.SaveOrUpdate(new UserSession(aCart, Clock.TimeNow(), aClient));

            transaction.Commit();

            return aCart;
        }

        private Client GetClient(Guid clientId, string password, ISession session)
        {
            Client aClient =
                session.QueryOver<Client>()
                    .Where(client => client.Id == clientId && client.Password == password)
                    .SingleOrDefault<Client>();

            return aClient;
        }

        public Cart AddAQuantityOfAnItem(int quantity, string aBook, Guid aCartId)
            //TODO: NO OLVIDAR AGREGAR LOS TRY CATCH
        {
            ISession session = SessionManager.OpenSession();
            ITransaction transaction = session.BeginTransaction();

            var userSession = GetAndVerifyUserSessionExpired(aCartId, session);
            Cart aCart = userSession.Cart;
            aCart.AddItemSomeTimes(aBook, quantity);
            userSession.UpdateLastActionTime(Clock.TimeNow());
            session.SaveOrUpdate(userSession);

            transaction.Commit();
            return aCart;
        }

        private UserSession GetAndVerifyUserSessionExpired(Guid aCartId, ISession session)
        {
            UserSession userSession =
                session.QueryOver<UserSession>().Where(uS => uS.Cart.Id == aCartId).SingleOrDefault<UserSession>();

            userSession.VerifyCartExpired(Clock.TimeNow());
            return userSession;
        }

        public Cart GetCart(Guid aCartId)
        {
            ISession session = SessionManager.OpenSession();

            Cart cart = session.Get<Cart>(aCartId);

            return cart;
        }

        public List<Sale> PurchasesFor(Client aClient)
        {
            ISession session = SessionManager.OpenSession();

            List<Sale> sales =
                session.QueryOver<Sale>().Where(sale => sale.Client.Id == aClient.Id).List<Sale>().ToList();

            return sales;
        }

        public Sale CheckoutCart(Guid aCartId, CreditCard aCreditCard, IDictionary aCatalog)
        {
            ISession session = SessionManager.OpenSession();
            ITransaction transaction = session.BeginTransaction();

            var userSession = GetAndVerifyUserSessionExpired(aCartId, session);
            var aCart = userSession.Cart;
            var aClient = userSession.Client;

            Cashier aCashier = new Cashier();
            Sale aSale = aCashier.CheckoutFor(aCreditCard, aCart, aCatalog, aClient);
            session.SaveOrUpdate(aSale);
            session.Delete(userSession);

            transaction.Commit();
            return aSale;
        }

        public bool IsRegistered(Sale sale)
        {
            ISession session = SessionManager.OpenSession();

            Sale aSale = session.Get<Sale>(sale.Id);

            return aSale != null;
        }

        public bool PurchasesContainsASaleForAClient(Sale aSale, Client aClient)
        {
            ISession session = SessionManager.OpenSession();

            Sale sale = session.QueryOver<Sale>().Where(saleBd => (saleBd.Id == aSale.Id) && (saleBd.Client.Id == aClient.Id)).List<Sale>().ToList().SingleOrDefault();

            return sale != null;
        }

        public IDictionary ListCart(Guid aCartId)
        {
            Cart cart = GetCart(aCartId);
            return cart.ListBooksWithOccurrences();
        }

        public bool ContainsThisQuantityOfBook(Guid aCartId, string aBook, int quantity)
        {
            return (int)ListCart(aCartId)[aBook] == quantity;
        }

        public bool CanHandle(string environment)
        {
            return environment == GlobalConfiguration.ProductionEnvironment;
        }

        public Client Login(string userName, string password)
        {
            ISession session = SessionManager.OpenSession();

            Client aClient =
                session.QueryOver<Client>()
                    .Where(each => each.UserName == userName && each.Password == password)
                    .SingleOrDefault<Client>();

            if (aClient == null)
                throw new ArgumentException("Invalid user or password");

            return aClient;
        }

        public void RegisterClient(string userName, string password)
        {
            ISession session = SessionManager.OpenSession();
            ITransaction transaction = session.BeginTransaction();

            VerifyNotExistUserName(userName, session);
            Client aClient = new Client(userName, password);
            session.SaveOrUpdate(aClient);

            transaction.Commit();
        }

        public void DeleteUser(string userName, string password) //TODO mirarlo
        {
            ISession session = SessionManager.OpenSession();
            ITransaction transaction = session.BeginTransaction();

            VerifyExistUserName(userName, session);

            Client aClient = session.QueryOver<Client>().Where(each => each.UserName == userName && each.Password == password).SingleOrDefault<Client>(); //TODO Query repetida

            /* Chanchada para salir del paso: */
            try
            {
                UserSession userSession = session.QueryOver<UserSession>().Where(uS => uS.Client.Id == aClient.Id).SingleOrDefault<UserSession>();
                session.Delete(userSession);
            }
            catch (Exception)
            {
                /* puto! */
            }
            try
            {
                Sale sale = session.QueryOver<Sale>().Where(s => s.Client.Id == aClient.Id).SingleOrDefault<Sale>();
                session.Delete(sale);
            }
            catch (Exception)
            {
 
            }

            session.Delete(aClient);
            transaction.Commit();
        }

        private void VerifyExistUserName(string userName, ISession session)
        {
            Client aClient = session.QueryOver<Client>().Where(each => each.UserName == userName).SingleOrDefault<Client>();
            if (aClient == null)
                throw new ArgumentException("Inexistent user");
        }

        private void VerifyNotExistUserName(string userName, ISession session)
        {
            Client aClient = session.QueryOver<Client>().Where(each => each.UserName == userName).SingleOrDefault<Client>();
            if (aClient != null)
                throw new ArgumentException("User already registered");
        }
    }
}