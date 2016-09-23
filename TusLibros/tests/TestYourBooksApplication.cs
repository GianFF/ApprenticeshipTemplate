using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TusLibros.app;
using TusLibros.model.entities;
using TusLibros.tests.support;
using FluentNHibernate.Conventions;

namespace TusLibros.tests
{
    [TestClass]
    public class TestYourBooksApplication
    {
        private TestObjectProvider objectProvider;

        [TestInitialize]
        public void SetUp()
        {
            objectProvider = new TestObjectProvider();
            
        }

        [TestMethod]
        public void Test00CanRegisterAndLoginAnUSer()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            
            Assert.IsTrue(aClient.UserName == "marcos" && aClient.Password == "123");
        }

        [TestMethod]
        public void Test01CanGetAnEmptyCart()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Cart cart = application.CreateCart(aClient.Id, aClient.Password);

            Assert.IsTrue(cart.IsEmpty());
        }

        [TestMethod]
        public void Test02CanAddABookInACart()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Cart aCart = application.CreateCart(aClient.Id, aClient.Password);
            aCart = application.AddAQuantityOfAnItem(1, objectProvider.ABook(), aCart.Id);

            Assert.IsFalse(aCart.IsEmpty());
        }

        [TestMethod]
        public void Test03WhenAddABookInACartThenTheBookIsInTheCart()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            string aBook = objectProvider.ABook();

            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Cart aCart = application.CreateCart(aClient.Id, aClient.Password);
            aCart = application.AddAQuantityOfAnItem(1, aBook, aCart.Id);

            Assert.IsTrue(aCart.HasABook(aBook));
        }

        [TestMethod]
        public void Test04After30MinutesCanNotAddABookInTheCart()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Cart aCart = application.CreateCart(aClient.Id, aClient.Password);
            string aBook = objectProvider.ABook();
            string otherBook = objectProvider.OtherBook();

            application.AddAQuantityOfAnItem(1, aBook, aCart.Id);
            application.Clock.UpdateSomeMinutes(30); // minutes
            
            try
            {
                application.AddAQuantityOfAnItem(1, otherBook, aCart.Id);
                Assert.Fail();
            }
            catch (TimeoutException e)
            {
                Assert.AreEqual("The cart has been expired", e.Message);

                aCart = application.GetCart(aCart.Id);
                Assert.IsFalse(aCart.HasABook(otherBook));
            }
        }

        [TestMethod]
        public void Test05CanAddABookAfter32MinutesWhenTheLastUsageOfTheCartWasBeforeHisExpiration()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Cart aCart = application.CreateCart(aClient.Id, aClient.Password);
            string aBook = objectProvider.ABook();

            application.Clock.UpdateSomeMinutes(20); // minutes
            aCart = application.AddAQuantityOfAnItem(1, aBook, aCart.Id);
            
            aCart = application.AddAQuantityOfAnItem(1, aBook, aCart.Id);
            application.Clock.UpdateSomeMinutes(12); // minutes
            
            aCart = application.AddAQuantityOfAnItem(1, aBook, aCart.Id);

            Assert.IsTrue(aCart.HasABook(aBook));
        }

        [TestMethod]
        public void Test06CanCheckoutACartWithOneBookWithASpecificCatalog()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Cart aCart = application.CreateCart(aClient.Id, aClient.Password);

            aCart = application.AddAQuantityOfAnItem(1, objectProvider.ABook(), aCart.Id);

            Sale sale = application.CheckoutCart(aCart.Id, objectProvider.AValidCreditCard(), objectProvider.ACatalog());

            Assert.IsTrue(application.IsRegistered(sale));
        }

        [TestMethod]
        public void Test07CanNotCheckoutACartWithAnInvalidBookWithASpecificCatalog()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Cart aCart = application.CreateCart(aClient.Id, aClient.Password);

            aCart = application.AddAQuantityOfAnItem(1, objectProvider.AnInvalidBook(), aCart.Id);

            try
            {
                application.CheckoutCart(aCart.Id, objectProvider.AValidCreditCard(), objectProvider.ACatalog());
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("The cart has invalid books!", e.Message);
            }
        }


        [TestMethod]
        public void Test08WhenAClientHasNoPurchasesThenHisListOfPurchasesIsEmpty()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = objectProvider.AClient();
            
            Assert.IsTrue(application.PurchasesFor(aClient).IsEmpty());
        }

        [TestMethod]
        public void Test09WhenAClientHasPurchasesThenHisPurchasesIsNotEmpty()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Cart aCart = application.CreateCart(aClient.Id, aClient.Password);
            aCart = application.AddAQuantityOfAnItem(1, objectProvider.ABook(), aCart.Id);
            application.CheckoutCart(aCart.Id, objectProvider.AValidCreditCard(), objectProvider.ACatalog());

            Assert.IsFalse(application.PurchasesFor(aClient).IsEmpty());
        }

        [TestMethod]
        public void Test10WhenAClientBuyABookHisPurchasesIsRegistered()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Cart aCart = application.CreateCart(aClient.Id, aClient.Password);
            aCart = application.AddAQuantityOfAnItem(1, objectProvider.ABook(), aCart.Id);
            Sale aSale = application.CheckoutCart(aCart.Id, objectProvider.AValidCreditCard(), objectProvider.ACatalog());

            Assert.IsTrue(application.PurchasesContainsFor(aSale, aClient));
        }

        [TestMethod]
        public void Test11WhenGetListCartOfAnEmptyCartThenIsEmpty()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Cart aCart = application.CreateCart(aClient.Id, aClient.Password);

            Assert.IsTrue(application.ListCart(aCart.Id).Count == 0);
        }

        [TestMethod]
        public void Test12WhenAddBookToCartAndGetListCartThenIsNotEmpty()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Cart aCart = application.CreateCart(aClient.Id, aClient.Password);
            aCart = application.AddAQuantityOfAnItem(1, objectProvider.ABook(), aCart.Id);

            Assert.IsFalse(application.ListCart(aCart.Id).Count == 0);
        }

        [TestMethod]
        public void Test13WhenAddAQuantityTimesABookToCartAndGetListCartThereAreTheBookWithThisQuantity()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Cart aCart = application.CreateCart(aClient.Id, aClient.Password);
            String aBook = objectProvider.ABook();
            aCart = application.AddAQuantityOfAnItem(4, aBook, aCart.Id);

            Assert.IsTrue(application.ContainsThisQuantityOfBook(aCart.Id, aBook, 4));
        }
    }
}
