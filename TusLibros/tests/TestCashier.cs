using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TusLibros.lib;
using TusLibros.tests.support;

namespace TusLibros.tests
{
    [TestClass]
    public class TestCashier
    {
        private ObjectProvider objectProvider;
        protected Cashier cashier;
        protected Cart cart;
        protected Cart aCartWithOneBook;
        protected Cart aCartWithTwoBook;
        protected Cart aCartWithTwoBookDiferents;
        protected MerchantProcessor merchantProcessor;
        protected CreditCard aclient;
        protected CreditCard anInvalidClient;

        [TestInitialize]
        public void SetUp()
        {
            objectProvider = new ObjectProvider();
            merchantProcessor = objectProvider.AnMerchantProcessor();
            cashier = objectProvider.ACashier(merchantProcessor);
            cart = objectProvider.EmptyCart();
            aCartWithOneBook = objectProvider.ACartWithOneBook();
            aCartWithTwoBook = objectProvider.ACartWithTwoBooks();
            aCartWithTwoBookDiferents = objectProvider.ACartWithTwoDiferentsBooks();
            aclient = objectProvider.AValidCreditCard();
            anInvalidClient = objectProvider.AnInvalidCreditCard();
        }
        
        [TestMethod]
        public void Test01CanNotCheckoutEmtpyCart()
        {
            try
            {
                cashier.PriceFor(cart);
                Assert.Fail();
            }
            catch (CannotCheckoutFor e)
            {
                Assert.AreEqual("The cart cannot be empty for checkout", e.Message);
            }
        }

        [TestMethod]
        public void Test02WhitACartWithOneBookHisPriceForItIs20()
        {
            Assert.AreEqual(20, cashier.PriceFor(aCartWithOneBook));                      
        }

        [TestMethod]
        public void Test03WhitACartWithtwoBookEqualsHisPriceForItIs40()
        {
            Assert.AreEqual(40, cashier.PriceFor(aCartWithTwoBook));
        }

        [TestMethod]
        public void Test04WhitACartWithtwoBookDiferentsHisPriceForItIs50()
        {
            Assert.AreEqual(50, cashier.PriceFor(aCartWithTwoBookDiferents));
        }

        [TestMethod]
        public void Test05WithACartWithOneBook()
        {
            cashier.CheckoutFor(aclient, aCartWithOneBook);
            TestAssertThatIsRegisteredTheSale(cashier, aCartWithOneBook);
            TestAssertTheLastOperationSuccesfullIsTheRight(cashier, 20);            
        }

        [TestMethod]
        public void TestAssertThatIsRegisteredTheSale(Cashier aCashier, Cart aSale)
        {
            Assert.IsTrue(aCashier.IsRegistered(aSale));
        }

        public void TestAssertTheLastOperationSuccesfullIsTheRight(Cashier aCashier, int mountOfOperation)
        {
            Assert.AreEqual(mountOfOperation, merchantProcessor.LastSuccessfulOperationFor(aclient));
        }

        [TestMethod]
        public void Test06WithACartWithTwoBooksEquals()
        {
            cashier.CheckoutFor(aclient, aCartWithTwoBook);
            TestAssertTheLastOperationSuccesfullIsTheRight(cashier, 40);
        }

        [TestMethod]
        public void Test07WithAnInvalidCreditCard()
        {
            try
            {
                cashier.CheckoutFor(anInvalidClient, aCartWithOneBook);
                Assert.Fail();
            }
            catch (CannotCheckoutFor e)
            {
                Assert.AreEqual("The credit card is expired", e.Message);
                TestAssertThatIsNotRegisteredTheSale(cashier, aCartWithOneBook);
            }
        }

        [TestMethod]
        public void TestAssertThatIsNotRegisteredTheSale(Cashier aCashier , Cart aSale )
        {
            Assert.IsFalse(aCashier.IsRegistered(aSale));
        }
    }  
}

/*context 'con una tarjeta expirada' do
      

      it 'lanza una excepcion: Tarjeta de credito expirada!' do
        expect{subject}.to raise_error RuntimeError, 'Tarjeta de credito expirada!'
      end
    end
    */
