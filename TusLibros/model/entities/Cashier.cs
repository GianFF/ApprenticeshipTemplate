using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

namespace TusLibros.model.entities
{
    public class Cashier
    {
        public virtual Guid Id { get; protected set; }

        public Cashier(){}

        public int PriceFor(Cart aCart, IDictionary<string,int> aCatalog)
        {
            AssertThatTheCartIsNotEmpty(aCart);
            IList<int> productPrices = aCart.MapItemsToPrices(aCatalog);
            return SumPricesIn(productPrices);
        }

        protected int SumPricesIn(IList<int> productPrices)
        {
            return productPrices.Sum();
        }

        public Sale CheckoutFor(CreditCard aCreditCard, Cart aCart, IDictionary<string,int> aCatalog, Client aClient, MerchantProcessor merchantProcessor)
        {
            VerifyIfTheCreditCardIsInvalid(aCreditCard);
            VerifyIfTheCartHasValidBooks(aCart, aCatalog);
            merchantProcessor.RegisterTransaction(aCreditCard, PriceFor(aCart, aCatalog));

            return new Sale(aCreditCard, CreateSaleDetail(aCart, aCatalog), aClient, DateTime.Now);
        }

        private List<SaleDetail> CreateSaleDetail(Cart aCart, IDictionary<string, int> aCatalog)
        {
            var saleDetails = aCart.CollectItems(item => new SaleDetail(item, aCart.QuantityOf(item), aCatalog[item]));
            return (List<SaleDetail>)saleDetails;
        }

        private void VerifyIfTheCartHasValidBooks(Cart aCart, IDictionary<string, int> aCatalog)
        {
            bool hasInvalidBook = aCart.VerifyIfContainsInvalidBooks(aCatalog);

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
