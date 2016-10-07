using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using TusLibros.clocks;
using TusLibros.db;
using TusLibros.model;
using TusLibros.model.entities;
using TusLibros.model.exceptions;

namespace TusLibros.app
{
    public class PersistentYourBooksApplication : IYourBooksApplication
    {
        public IClock Clock { get; set; }

        public PersistentYourBooksApplication(IClock aClock)
        {
            Clock = aClock;
        }

        public Guid CreateCart(Guid clientId, String password)
        {
            Cart aCart = new Cart();

            ISession session = SessionManager.OpenSession();
            ITransaction transaction = session.BeginTransaction();

            Client aClient = GetClient(clientId, password, session);
            UserSession userSession = new UserSession(Clock.TimeNow(), aClient, aCart);
            session.SaveOrUpdate(userSession);

            transaction.Commit();

            return userSession.CartId; //por el momento manejo esto como GUID porque no se ocurre otra cosa.
        }       

        public void AddAQuantityOfAnItem(int quantity, string aBook, Guid aCartId) // TODO: NO OLVIDAR AGREGAR LOS TRY CATCH
        {
            ISession session = SessionManager.OpenSession();
            ITransaction transaction = session.BeginTransaction();

            var userSession = GetAndVerifyUserSessionExpired(aCartId, session);
            userSession.AddQuantityOfAnItem(aBook,quantity, Clock);            
            session.SaveOrUpdate(userSession);

            transaction.Commit();
        }

        public Tuple<IDictionary, int> PurchasesFor(Client aClient)
        {
            ISession session = SessionManager.OpenSession();

            var sales = GetSalesByClientId(aClient, session);

            Tuple<IDictionary, int> detailsPurchases = Tuple.Create(BooksAndQuantities(sales), TotalAmountFor(sales));

            return detailsPurchases;
        }

        private int TotalAmountFor(List<Sale> sales)
        {
            return sales.Sum(aSale => aSale.Total());
        }

        private IDictionary BooksAndQuantities(List<Sale> sales)
        {
            var listBooksWithOccurrences = new Dictionary<string, int>();

            sales.ForEach(aSale => aSale.AddBooksWithOcurrencies(listBooksWithOccurrences));

            return listBooksWithOccurrences;
        }

        public Guid CheckoutCart(Guid aCartId, CreditCard aCreditCard, IDictionary<string, int> aCatalog)
        {
            ISession session = SessionManager.OpenSession();
            ITransaction transaction = session.BeginTransaction();

            var userSession = GetAndVerifyUserSessionExpired(aCartId, session);
            var aCart = userSession.Cart;
            var aClient = userSession.Client;

            Cashier aCashier = new Cashier();
            Sale aSale = aCashier.CheckoutFor(aCreditCard, aCart, aCatalog, aClient, GlobalConfiguration.MerchantProcessor);
            session.SaveOrUpdate(aSale);
            session.Delete(userSession);

            transaction.Commit();
            return aSale.TransactionId;
        }

        public IDictionary<string, int> ListCart(Guid aCartId)
        {
            Cart cart = GetCart(aCartId);
            return cart.Items;
        }

        public Client Login(string userName, string password)
        {
            ISession session = SessionManager.OpenSession();

            var aClient = GetClientByUserNameAndPassword(userName, password, session);

            if (aClient == null)
                throw new LoginException("Invalid user or password");

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
                throw new RegisterException("User already registered");
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

        public Sale GetSale(Guid transactionId)
        {
            ISession session = SessionManager.OpenSession();

            Sale sale = session.QueryOver<Sale>().Where(aSale => aSale.TransactionId == transactionId).SingleOrDefault<Sale>();

            return sale;
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
            var userSession = GetUserSessionByCartId(aCartId, session);

            userSession.VerifyCartExpired(Clock.TimeNow());
            return userSession;
        }

        private static UserSession GetUserSessionByCartId(Guid aCartId, ISession session)
        {
            UserSession userSession =
                session.QueryOver<UserSession>().Where(uS => uS.CartId == aCartId).SingleOrDefault<UserSession>();
            return userSession;
        }

        public Cart GetCart(Guid aCartId)
        {
            ISession session = SessionManager.OpenSession();

            UserSession userSession = GetUserSessionByCartId(aCartId, session);

            return userSession.Cart;
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
            List<Sale> sales = session.QueryOver<Sale>().Where(sale => sale.Client.Id == aClient.Id).List<Sale>().ToList();
            return sales;
        }

    }
}