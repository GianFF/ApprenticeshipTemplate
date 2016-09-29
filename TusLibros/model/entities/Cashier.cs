using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Util;
using TusLibros.app;
using TusLibros.app.environment;

namespace TusLibros.model.entities
{
    public class Cashier
    {
        public virtual Guid Id { get; protected set; }

        public Cashier()
        {
        }

        public int PriceFor(Cart aCart, IDictionary aCatalog)
        {
            AssertThatTheCartIsNotEmpty(aCart);
            IEnumerable<int> productPrices = aCart.Items.Select(aProduct => (int) aCatalog[aProduct]);
            return SumPricesIn(productPrices);
        }

        protected int SumPricesIn(IEnumerable<int> productPrices)
        {
            return productPrices.Sum();
        }

        public Sale CheckoutFor(CreditCard aCreditCard, Cart aCart, IDictionary aCatalog, Client aClient, MerchantProcessor merchantProcessor)
        {
            VerifyIfTheCreditCardIsInvalid(aCreditCard);
            VerifyIfTheCartHasValidBooks(aCart, aCatalog);
            merchantProcessor.RegisterTransaction(aCreditCard, PriceFor(aCart, aCatalog));

            return new Sale(aCreditCard, CreateSaleDetail(aCart, aCatalog), aClient, DateTime.Now);
        }

        private List<SaleDetail> CreateSaleDetail(Cart aCart, IDictionary aCatalog) //TODO: preguntar a quien le corresponde "armar" el detalle.
        {
            return aCart.CreateSaleDetailWith(aCatalog);
        }

        private void VerifyIfTheCartHasValidBooks(Cart aCart, IDictionary aCatalog)
        {
            bool hasInvalidBook = aCart.Items.Any(book => !aCatalog.Contains(book));

            if (hasInvalidBook)
            {
                throw new ArgumentException("The cart has invalid books!");
            }
        }

        private void VerifyIfTheCreditCardIsInvalid(CreditCard aCreditCard)
        {
            if (aCreditCard.IsExpired())
            {
                throw new ArgumentException("The credit card is expired");
            }
        }

        private void AssertThatTheCartIsNotEmpty(Cart aCart)
        {
            if (aCart.IsEmpty())
            {
                throw new ArgumentException("The cart cannot be empty for checkout");
            }
        }
    }   
}
