using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Util;

namespace TusLibros.model.entities
{
    public class Cashier
    {
        public virtual Guid Id { get; protected set; }
        public virtual MerchantProcessor AMerchantProcessor { get; set; }

        public Cashier(MerchantProcessor aMerchantProcessor)
        {
            AMerchantProcessor = aMerchantProcessor;
        }

        public int PriceFor(Cart aCart, Hashtable aCatalog)
        {
            AssertThatTheCartIsNotEmpty(aCart);
            IEnumerable<int> productPrices = aCart.Items.Select(aProduct => (int) aCatalog[aProduct]);
            return SumPricesIn(productPrices);
        }

        protected int SumPricesIn(IEnumerable<int> productPrices)
        {
            return productPrices.Sum();
        }

        public Sale CheckoutFor(CreditCard aCreditCard, Cart aCart, Hashtable aCatalog, Client aClient)
        {
            VerifyIfTheCreditCardIsInvalid(aCreditCard);
            VerifyIfTheCartHasValidBooks(aCart, aCatalog);
            AMerchantProcessor.RegisterTransaction(aCreditCard, PriceFor(aCart, aCatalog));

            return new Sale(aCreditCard, CatalogSubset(aCart, aCatalog), aClient);
        }

        private static Hashtable CatalogSubset(Cart aCart, Hashtable aCatalog)
        {
            var CatalogSubset = new Hashtable();

            aCart.Items.ForEach(book => CatalogSubset.Add(book, aCatalog[book]));

            return CatalogSubset;
        }

        private void VerifyIfTheCartHasValidBooks(Cart aCart, Hashtable aCatalog)
        {
            bool hasInvalidBook = aCart.Items.Any(book => !aCatalog.ContainsKey(book));

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
