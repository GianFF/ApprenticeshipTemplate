using System;
using System.Collections;
using System.Collections.Generic;

namespace TusLibros.lib
{
    class Cashier
    {
        protected List<Cart> SalesRecord;
        protected Hashtable Catalog;
        protected MerchantProcessor AMerchantProcessor;

        //protected List<string> Items = new List<string>();
        public Cashier(MerchantProcessor aMerchantProcessor)
        {
            SalesRecord = new List<Cart>();
            Catalog = new Hashtable();
            Catalog.Add("nacidos de la bruma", 20);
            Catalog.Add("nacidos de la bruma el imperio final", 30);
            AMerchantProcessor = aMerchantProcessor;
        }

        protected int PriceFor(Cart aCart)
        {
            AssertThatTheCartIsNotEmpty(aCart);
            return SumPricesIn(aCart.GetItems());//.map { | un_libro | @catalogo[un_libro] } # TODO ver
        }

        protected int SumPricesIn(List<String> aProductList)
        {
            int sumOfProducts = 0;
            foreach (var aProduct in aProductList)
            {
                sumOfProducts += PriceForProduct(aProduct);
            }
            return sumOfProducts;
        }

        protected int PriceForProduct(String aProduct)
        {
            return (int) Catalog[aProduct];
        }

        private void AssertThatTheCartIsNotEmpty(Cart aCart)
        {
            if (aCart.IsEmpty())
            {
                throw new System.NotImplementedException();//Ver esto
            }            
        }

        public void CheckoutFor(CreditCard aCreditCard, Cart aCart)
        {
            VerifyIfTheCreditCardIsInvalid(aCreditCard);
            AMerchantProcessor.RegisterTransaction(aCreditCard, PriceFor(aCart));
            SalesRecord.Add((aCart));
        }



        private void VerifyIfTheCreditCardIsInvalid(CreditCard aCreditCard)
        {
            if (aCreditCard.IsExpired())
            {
                throw new NotImplementedException();
            }
        }

        public List<Cart> GetSalesRecord()
        {
            return SalesRecord;
        }

        public bool IsRegistered(Cart aSale)
        {
            return SalesRecord.Contains(aSale);
        }
    }
}
