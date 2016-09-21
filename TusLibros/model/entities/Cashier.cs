using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TusLibros.model.entities
{
    public class Cashier
    {
        public virtual Guid Id { get; protected set; }
        public virtual IList<Cart> SalesRecord { get; set; }
        public virtual MerchantProcessor AMerchantProcessor { get; set; }

        public Cashier(MerchantProcessor aMerchantProcessor)
        {
            SalesRecord = new List<Cart>();
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

        public void CheckoutFor(CreditCard aCreditCard, Cart aCart, Hashtable aCatalog)
        {
            VerifyIfTheCreditCardIsInvalid(aCreditCard);
            VerifyIfTheCartHasValidBooks(aCart, aCatalog);
            AMerchantProcessor.RegisterTransaction(aCreditCard, PriceFor(aCart, aCatalog));
            SalesRecord.Add(aCart);
        }

        public bool IsRegistered(Cart aSale)
        {
            return SalesRecord.Contains(aSale);
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
