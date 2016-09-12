using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TusLibros.lib;

namespace TusLibros.tests.support {
    class ObjectProvider
    {
        public String AnInvalidBook()
        {
            return "Book from other editorial";
        }

        public String ABook()
        {
            return "nacidos de la bruma";
        }

        public Cart EmptyCart()
        {
            return new Cart();
        }
        
        public String OtherBook()
        {
            return "nacidos de la bruma el imperio final";
        }

        public Cart ACartWithOneBook()
        {
            Cart cart = this.EmptyCart();
            cart.AddItem(this.ABook());
            return cart;
        }

        public Cart ACartWithTwoBooks()
        {
            Cart cart = new Cart();
            cart.AddItemSomeTimes(this.ABook(), 2);
            return cart;
        }

        public Cashier ACashier(MerchantProcessor aMerchantProcessor)
        {
            return new Cashier(aMerchantProcessor);
        }

        public Cart ACartWithTwoDiferentsBooks()
        {
            Cart cart = new Cart();
            cart.AddItem(this.ABook());
            cart.AddItem(this.OtherBook());
            return cart;
        }

        public CreditCard AnInvalidCreditCard()
        {
            DateTime now = DateTime.Now;
            DateTime anyoneDate = new DateTime(1900, 4, 10);

            TimeSpan diffBetweenTwoDates = now.Subtract(anyoneDate);
            DateTime lastDate = now.Subtract(diffBetweenTwoDates);

            return new CreditCard(lastDate);        
        }

        public CreditCard AValidCreditCard()
        {
            DateTime now = DateTime.Now;           
            DateTime dateOfExpiration = now.AddMonths(2);
            CreditCard creditCard = new CreditCard(dateOfExpiration);

            return creditCard;
        }

        public FacadeYourBooks AFacade()
        {
            return new FacadeYourBooks();
        }

        public MerchantProcessor AnMerchantProcessor()
        {
            return new MerchantProcessor();
        }

        public String AnInvalidUser()
        {
            return "invalid user";
        }

        public String AValidUser()
        {
            return "valid user";
        }
    }
}
