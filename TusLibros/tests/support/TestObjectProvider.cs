using System;
using System.Collections;
using System.Collections.Generic;
using TusLibros.app;
using TusLibros.app.environment;
using TusLibros.model.entities;

namespace TusLibros.tests.support {
    public class TestObjectProvider
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
            cart.AddItemSomeTimes(this.ABook(),1);
            return cart;
        }

        public Cart ACartWithTwoBooks()
        {
            Cart cart = new Cart();
            cart.AddItemSomeTimes(this.ABook(), 2);
            return cart;
        }

        public Cashier ACashier()
        {
            return new Cashier();
        }

        public Cart ACartWithTwoDiferentsBooks()
        {
            Cart cart = new Cart();
            cart.AddItemSomeTimes(this.ABook(),1);
            cart.AddItemSomeTimes(this.OtherBook(),1);
            return cart;
        }

        public CreditCard AnInvalidCreditCard()
        {
            DateTime now = DateTime.Now;
            DateTime anyoneDate = new DateTime(1900, 4, 10);

            TimeSpan diffBetweenTwoDates = now.Subtract(anyoneDate);
            DateTime lastDate = now.Subtract(diffBetweenTwoDates);

            return new CreditCard(lastDate, 123456);        
        }

        public CreditCard AValidCreditCard()
        {
            DateTime now = DateTime.Now;           
            DateTime dateOfExpiration = now.AddMonths(2);
            CreditCard creditCard = new CreditCard(dateOfExpiration, 123456);

            return creditCard;
        }

        public IYourBooksApplication YourBooksApplication() //TODO: ver si hay alguna manera de correr los tests en los dos ambientes.
        {
            DevelopmentEnvironment enviroment = new DevelopmentEnvironment(new TransientDataBaseStrategy());
            //DevelopmentEnvironment enviroment = new DevelopmentEnvironment(new PersitentDataBaseStrategy());
            return enviroment.GetApplication();
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

        public IDictionary ACatalog()
        {
            var catalog = new Dictionary<string,int>();

            catalog.Add("nacidos de la bruma", 20);
            catalog.Add("nacidos de la bruma el imperio final", 30);

            return catalog;
        }

        public Client AClient()
        {
            var client = new Client("carlitos", "123");
            return client;
        }
    }
}
