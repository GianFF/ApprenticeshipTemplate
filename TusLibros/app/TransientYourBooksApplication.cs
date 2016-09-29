using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Cfg.XmlHbmBinding;
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

        public Guid CreateCart(Guid clientId, String password)
        {
            Client aClient = GetClient(clientId, password);
            UserSession userSession = new UserSession(Clock.TimeNow(), aClient);
            UserSessions.Add(userSession);
            return userSession.CartId; 
        }
        
        public void AddAQuantityOfAnItem(Guid aCartId, string aBook, int quantity)
        {
            UserSession userSession = FindUserSessionByCartId(aCartId);
            userSession.VerifyCartExpired(Clock.TimeNow());

            userSession.AddQuantityOfAnItem(aBook, quantity);
            UserSessions.Add(userSession); 
        }

        public IDictionary ListCart(Guid aCartId)
        {
            UserSession userSession = FindUserSessionByCartId(aCartId);
            return userSession.ListCart();
        }

        public Guid CheckoutCart(Guid aCartId, CreditCard aCreditCard, IDictionary aCatalog) //TODO: sacar el catalogo de acá.
        {
            UserSession userSession = FindUserSessionByCartId(aCartId);
            Sale aSale = userSession.CheckoutCartWith(aCreditCard, MerchantProcessor, aCatalog);
            Sales.Add(aSale);

            return aSale.TransactionId;
        }

        public Tuple<IDictionary, int> PurchasesFor(Client aClient)
        {
            List<Sale> sales = Sales.FindAll(sale => sale.ForClient(aClient));

            Tuple<IDictionary, int> detailsPurchases = Tuple.Create(BooksAndQuantities(sales), TotalAmountFor(sales));

            return detailsPurchases;
        }

        private IDictionary BooksAndQuantities(List<Sale> sales)
        {
            var listBooksWithOccurrences = new Dictionary<string, int>();

            sales.ForEach(aSale => aSale.AddBooksWithOcurrencies(listBooksWithOccurrences));

            return listBooksWithOccurrences;
        }

        private int TotalAmountFor(List<Sale> sales)
        {
            return sales.Sum(aSale => aSale.Total());
        }

        public bool IsSaleRegistered(Sale aSale)
        {
            return Sales.Contains(aSale);
        }
        public bool PurchasesContainsASaleForAClient(Sale aSale, Client aClient)
        {
            return PurchasesFor(aClient).Contains(aSale);
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

        private UserSession FindUserSessionByCartId(Guid aCartId)
        {
            return UserSessions.Find(session => session.CartId == aCartId);
        }

        public Cart GetCart(Guid aCartId)
        {
            UserSession userSession = FindUserSessionByCartId(aCartId);
            return userSession.Cart;
        }

        private Client GetClient(Guid clientId, string password)
        {
            Client aClient = Clients.Find(client => client.Id == clientId && client.Password == password);
            return aClient;
        }
    }
}