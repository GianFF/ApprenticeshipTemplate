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
        public MerchantProcessor MerchantProcessor { get; }


        public PersistentYourBooksApplication(IClock clock, MerchantProcessor merchantProcessor)
        {
            Clock = clock;
            MerchantProcessor = merchantProcessor;
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

        public List<Sale> PurchasesFor(Client aClient)
        {
            ISession session = SessionManager.OpenSession();

            var sales = GetSalesByClientId(aClient, session);

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
            Sale aSale = aCashier.CheckoutFor(aCreditCard, aCart, aCatalog, aClient, MerchantProcessor);
            session.SaveOrUpdate(aSale);
            session.Delete(userSession);

            transaction.Commit();
            return aSale;
        }

        public IDictionary ListCart(Guid aCartId)
        {
            Cart cart = GetCart(aCartId);
            return cart.ListBooksWithOccurrences();
        }

        public Client Login(string userName, string password)
        {
            ISession session = SessionManager.OpenSession();

            var aClient = GetClientByUserNameAndPassword(userName, password, session);

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

        public bool IsSaleRegistered(Sale sale)
        {
            ISession session = SessionManager.OpenSession();

            Sale aSale = session.Get<Sale>(sale.Id);

            return aSale != null;
        }

        public bool PurchasesContainsASaleForAClient(Sale aSale, Client aClient)
        {
            ISession session = SessionManager.OpenSession();

            Sale sale = session.QueryOver<Sale>().Where(saleBd => (saleBd.Id == aSale.Id) && (saleBd.Client.Id == aClient.Id)).SingleOrDefault();

            return sale != null;
        }

        public bool ContainsThisQuantityOfBook(Guid aCartId, string aBook, int quantity)
        {
            return (int)ListCart(aCartId)[aBook] == quantity;
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

        public void DeleteUser(string userName, string password) 
        {
            ISession session = SessionManager.OpenSession();
            ITransaction transaction = session.BeginTransaction();

            VerifyExistUserName(userName, session);

            var aClient = GetClientByUserNameAndPassword(userName, password, session);

            try
            {
                DeleteUserSession(session, aClient);
            }
            catch (Exception)
            {
                /* Do nothing */
            }
            try
            {
                DeleteSalesByClientId(session, aClient);
            }
            catch (Exception)
            {
                /* Do nothing */
            }

            session.Delete(aClient);
            transaction.Commit();
        }

        private static void DeleteSalesByClientId(ISession session, Client aClient)
        {
            var sales = GetSalesByClientId(aClient, session);
            sales.ForEach(aSale => session.Delete(aSale));
        }

        private static void DeleteUserSession(ISession session, Client aClient)
        {
            var userSession = GetUserSessionByClientId(session, aClient);
            session.Delete(userSession);
        }

        private static UserSession GetUserSessionByClientId(ISession session, Client aClient)
        {
            UserSession userSession =
                session.QueryOver<UserSession>().Where(uS => uS.Client.Id == aClient.Id).SingleOrDefault<UserSession>();
            return userSession;
        }

        private Client GetClient(Guid clientId, string password, ISession session)
        {
            Client aClient =
                session.QueryOver<Client>()
                    .Where(client => client.Id == clientId && client.Password == password)
                    .SingleOrDefault<Client>();

            return aClient;
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

        private static Client GetClientByUserNameAndPassword(string userName, string password, ISession session)
        {
            Client aClient =
                session.QueryOver<Client>()
                    .Where(each => each.UserName == userName && each.Password == password)
                    .SingleOrDefault<Client>();
            return aClient;
        }

        private static List<Sale> GetSalesByClientId(Client aClient, ISession session)
        {
            List<Sale> sales =
                session.QueryOver<Sale>().Where(sale => sale.Client.Id == aClient.Id).List<Sale>().ToList();
            return sales;
        }

    }
}