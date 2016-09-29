using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TusLibros.clocks;
using TusLibros.model;
using TusLibros.model.entities;

namespace TusLibros.app
{
    public class TransientYourBooksApplication : IYourBooksApplication
    {
        public List<UserSession> UserSessions { get; set; }
        public List<Sale> Sales { get; set; }
        public List<Client> Clients { get; set; }
        public IClock Clock { get; set; }

        public TransientYourBooksApplication(IClock aClock)
        {
            UserSessions = new List<UserSession>();
            Sales = new List<Sale>();
            Clients = new List<Client>();
            Clock = aClock;
        }

        public Guid CreateCart(Guid clientId, String password)
        {
            Client aClient = GetClient(clientId, password);
            UserSession userSession = new UserSession(Clock.TimeNow(), aClient);
            UserSessions.Add(userSession);
            return userSession.CartId; 
        }

        public void AddAQuantityOfAnItem(int quantity, string aBook, Guid aCartId)
        {
            UserSession userSession = FindUserSessionByCartId(aCartId);
            userSession.VerifyCartExpired(Clock.TimeNow());

            userSession.AddQuantityOfAnItem(aBook, quantity,Clock);

            UserSessions.Add(userSession);
        }

        public IDictionary<string, int> ListCart(Guid aCartId)
        {
            UserSession userSession = FindUserSessionByCartId(aCartId);
            return userSession.ListCart();
        }

        public Guid CheckoutCart(Guid aCartId, CreditCard aCreditCard, IDictionary<string, int> aCatalog) //TODO: sacar el catalogo de acá.
        {
            UserSession userSession = FindUserSessionByCartId(aCartId);
            Sale aSale = userSession.CheckoutCartWith(aCreditCard, GlobalConfiguration.MerchantProcessor, aCatalog);
            Sales.Add(aSale);

            return aSale.TransactionId;
        }

        public Tuple<IDictionary, int> PurchasesFor(Client aClient)
        {
            List<Sale> sales = Sales.FindAll(sale => sale.ForClient(aClient));

            Tuple<IDictionary, int> detailsPurchases = Tuple.Create(BooksAndQuantities(sales), TotalAmountFor(sales));

            return detailsPurchases;
        }

        public bool IsSaleRegistered(Sale aSale)
        {
            return Sales.Contains(aSale);
        }

        public bool PurchasesContainsASaleForAClient(Sale aSale, Client aClient)
        {
            List<Sale> sales = Sales.FindAll(sale => sale.ForClient(aClient));

            return sales.Contains(aSale);
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

        public Sale GetSale(Guid saleId)
        {
            return Sales.Find(aSale => aSale.TransactionId == saleId);
        }

        private Client GetClient(Guid clientId, string password)
        {
            Client aClient = Clients.Find(client => client.Id == clientId && client.Password == password);
            return aClient;
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
    }
}