using System;
using System.Collections;
using System.Collections.Generic;
using TusLibros.clocks;
using TusLibros.model;
using TusLibros.model.entities;

namespace TusLibros.app
{
    internal class TransientYourBooksApplication : IYourBooksApplication
    {
        public IClock Clock { get; set; }
        public List<UserSession> UserSessions { get; set; }
        public List<Sale> Sales { get; set; }
        public List<Client> Clients { get; set; }
        public MerchantProcessor MerchantProcessor { get; }

        public TransientYourBooksApplication(IClock clock, MerchantProcessor merchantProcessor)
        {
            UserSessions = new List<UserSession>();
            Sales = new List<Sale>();
            Clients = new List<Client>();
            Clock = clock;
            MerchantProcessor = merchantProcessor;
        }

        public Cart CreateCart(Guid clientId, String password)
        {
            Cart aCart = new Cart();
            Client aClient = GetClient(clientId, password);
            UserSessions.Add(new UserSession(aCart, Clock.TimeNow(), aClient));
            return aCart; 
        }
        
        public Cart AddAQuantityOfAnItem(int quantity, string aBook, Guid aCartId)
        {
            UserSession userSession = UserSession(aCartId);
            userSession.VerifyCartExpired(Clock.TimeNow());
            Cart aCart = userSession.Cart;
            aCart.AddItemSomeTimes(aBook, quantity);
            userSession.UpdateLastActionTime(Clock.TimeNow());
            return aCart;
        }        

        public List<Sale> PurchasesFor(Client aClient)
        {
            return Sales.FindAll(sale => sale.ForClient(aClient));
        }

        public Sale CheckoutCart(Guid aCartId, CreditCard aCreditCard, IDictionary aCatalog)
        {
            var aCart = GetCart(aCartId);
            Cashier aCashier = new Cashier();
            Client aClient = UserSession(aCartId).Client;
            Sale aSale = aCashier.CheckoutFor(aCreditCard, aCart, aCatalog, aClient, MerchantProcessor);

            Sales.Add(aSale);
            return aSale;
        }

        public bool IsSaleRegistered(Sale aSale)
        {
            return Sales.Contains(aSale);
        }
        public bool PurchasesContainsASaleForAClient(Sale aSale, Client aClient)
        {
            return PurchasesFor(aClient).Contains(aSale);
        }

        public IDictionary ListCart(Guid aCartId)
        {
            var aCart = GetCart(aCartId);
            return  aCart.ListBooksWithOccurrences();
        }

        public bool ContainsThisQuantityOfBook(Guid aCartId, string aBook, int quantityOfBook)
        {
            return (int)ListCart(aCartId)[aBook] == quantityOfBook;
        }

        public Client Login(string userName, string password)
        {
            return Clients.Find(client => client.SameUserNameAndPassword(userName, password));
        }

        public void RegisterClient(string userName, string password)
        {
            VerifyNotExistUserName(userName);
            Client aClient = new Client(userName,password);
            Clients.Add(aClient);
        }

        private void VerifyNotExistUserName(string userName)
        {
            bool existsUserName = Clients.Exists(client => client.SameUserName(userName));
            if (existsUserName)
                throw new ArgumentException("User already registered");
        }

        public void DeleteUser(string userName, string password)
        {
            Client aClient = Clients.Find(client => client.UserName == userName && client.Password == password);
            Clients.Remove(aClient);
        }

        private UserSession UserSession(Guid aCartId)
        {
            return UserSessions.Find(session => session.Cart.Id == aCartId);
        }

        public Cart GetCart(Guid aCartId)
        {
            UserSession userSession = UserSession(aCartId);
            return userSession.Cart;
        }

        private Client GetClient(Guid clientId, string password)
        {
            Client aClient = Clients.Find(client => client.Id == clientId && client.Password == password);
            return aClient;
        }
    }
}