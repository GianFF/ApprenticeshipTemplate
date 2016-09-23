using System;
using System.Collections;
using System.Collections.Generic;
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
            Client aClient = GetClient(clientId, password);
            session.SaveOrUpdate(new UserSession(aCart, Clock.TimeNow(), aClient));

            transaction.Commit();

            return aCart;
        }

        private Client GetClient(Guid clientId, string password)
        { 
            ISession session = SessionManager.OpenSession();
            
            Client aClient = session.QueryOver<Client>().Where(each => each.Id == clientId && each.Password == password).SingleOrDefault<Client>();
            
            return aClient;
        }

        public Cart AddAQuantityOfAnItem(int quantity, string aBook, Guid aCartId)//TODO: NO OLVIDAR AGREGAR LOS TRY CATCH
        {
            ISession session = SessionManager.OpenSession();
            ITransaction transaction = session.BeginTransaction();

            var userSession = GetAndVerifyUserSessionExpired(aCartId, session);
            Cart aCart = userSession.Cart;
            aCart.AddItemSomeTimes(aBook, quantity);
            session.SaveOrUpdate(userSession);

            transaction.Commit();
            return aCart;
        }

        private UserSession GetAndVerifyUserSessionExpired(Guid aCartId, ISession session)
        {
            UserSession userSession = session.QueryOver<UserSession>().Where(uS => uS.Cart.Id == aCartId).SingleOrDefault<UserSession>();

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
            throw new NotImplementedException();
        }

        public Sale CheckoutCart(Guid aCartId, CreditCard aCreditCard, IDictionary aCatalog)
        {
            ISession session = SessionManager.OpenSession();
            ITransaction transaction = session.BeginTransaction();

            var userSession = GetAndVerifyUserSessionExpired(aCartId, session);
            var aCart = userSession.Cart;
            var aClient = userSession.Client;
            
            Cashier aCashier = new Cashier(GlobalConfiguration.MerchantProcessor);
            Sale aSale = aCashier.CheckoutFor(aCreditCard, aCart, aCatalog, aClient);
            session.SaveOrUpdate(aSale); 
            session.Delete(userSession);
            
            transaction.Commit();
            return aSale;
        }

        public bool IsRegistered(Sale sale)
        {
            throw new NotImplementedException();
        }

        public bool PurchasesContainsFor(Sale aSale, Client aClient)
        {
            throw new NotImplementedException();
        }

        public IDictionary ListCart(Guid aCartId)
        {
            throw new NotImplementedException();
        }

        public bool ContainsThisQuantityOfBook(Guid aCartId, string aBook, int quantity)
        {
            throw new NotImplementedException();
        }

        public bool CanHandle(string environment)
        {
            return environment == GlobalConfiguration.GlobalProductionEnvironment;
        }

        public Client Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public void RegisterClient(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}