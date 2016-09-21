using System;
using FluentNHibernate.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TusLibros.model.entities;
using TusLibros.tests.support;

namespace TusLibros.tests
{
    [TestClass]
    public class TestCashier
    {
        private TestObjectProvider objectProvider;
        protected Cashier cashier;
        protected Cart cart;
        protected Cart aCartWithOneBook;
        protected Cart aCartWithTwoBook;
        protected Cart aCartWithTwoBookDiferents;
        protected MerchantProcessor merchantProcessor;
        protected CreditCard ACreditCard;
        protected CreditCard anInvalidCreditCard;

        [TestInitialize]
        public void SetUp()
        {
            objectProvider = new TestObjectProvider();
            merchantProcessor = objectProvider.AnMerchantProcessor();
            cashier = objectProvider.ACashier(merchantProcessor);
            cart = objectProvider.EmptyCart();
            aCartWithOneBook = objectProvider.ACartWithOneBook();
            aCartWithTwoBook = objectProvider.ACartWithTwoBooks();
            aCartWithTwoBookDiferents = objectProvider.ACartWithTwoDiferentsBooks();
            ACreditCard = objectProvider.AValidCreditCard();
            anInvalidCreditCard = objectProvider.AnInvalidCreditCard();
        }
        
        [TestMethod]
        public void Test01CanNotCheckoutEmtpyCart()
        {
            try
            {
                cashier.PriceFor(cart, objectProvider.ACatalog());
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("The cart cannot be empty for checkout", e.Message);
            }
        }

        [TestMethod]
        public void Test02WhitACartWithOneBookHisPriceForItIs20()
        {
            Assert.AreEqual(20, cashier.PriceFor(aCartWithOneBook, objectProvider.ACatalog()));                      
        }

        [TestMethod]
        public void Test03WhitACartWithtwoBookEqualsHisPriceForItIs40()
        {
            Assert.AreEqual(40, cashier.PriceFor(aCartWithTwoBook, objectProvider.ACatalog()));
        }

        [TestMethod]
        public void Test04WhitACartWithtwoBookDiferentsHisPriceForItIs50()
        {
            Assert.AreEqual(50, cashier.PriceFor(aCartWithTwoBookDiferents, objectProvider.ACatalog()));
        }

        [TestMethod]
        public void Test07WithAnInvalidCreditCard()
        {
            try
            {
                Sale sale = cashier.CheckoutFor(anInvalidCreditCard, aCartWithOneBook, objectProvider.ACatalog(), objectProvider.AClient());
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("The credit card is expired", e.Message);
            }
        }
    }  
}
