using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public TransientYourBooksApplication(IClock clock)
        {
            UserSessions = new List<UserSession>();
            Sales = new List<Sale>();
            Clock = clock;
        }

        public Cart CreateCart()
        {
            Cart aCart = new Cart();
            UserSessions.Add(new UserSession(aCart, Clock.TimeNow()));
            return aCart;
        }

        public void AddAQuantityOfAnItem(int quantity, string aBook, Guid aCartId)
        {
            UserSession userSession = UserSession(aCartId);
            userSession.VerifyCartExpired(Clock.TimeNow());
            Cart aCart = userSession.Cart;
            aCart.AddItemSomeTimes(aBook, quantity);
        }

        public Cart GetCart(Guid aCartId)
        {
            UserSession userSession = UserSession(aCartId);
            return userSession.Cart;
        }

        public List<Sale> PurchasesFor(Hashtable aClient)
        {
            return Sales.FindAll(sale => sale.ForClient(aClient));
        }

        public Sale CheckoutCart(Guid aCartId, CreditCard aCreditCard, Hashtable aCatalog, Hashtable aClient)
        {
            var aCart = GetCart(aCartId);
            Cashier aCashier = new Cashier(new MerchantProcessor()); // TODO: extraer al constructor el merchantProcessor o pasarlo por parametro en el metodo?. 
            Sale aSale = aCashier.CheckoutFor(aCreditCard, aCart, aCatalog, aClient);

            Sales.Add(aSale);
            return aSale;
        }

        public bool IsRegistered(Sale aSale)
        {
            return Sales.Contains(aSale);
        }

        private UserSession UserSession(Guid aCartId)
        {
            return UserSessions.Find(session => session.Cart.Id == aCartId);
        }
    }
}