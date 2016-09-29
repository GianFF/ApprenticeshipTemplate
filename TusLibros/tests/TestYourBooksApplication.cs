using System;
using FluentNHibernate.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TusLibros.app;
using TusLibros.model.entities;
using TusLibros.tests.support;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

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

        [TestCleanup]
        public void TearDown()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.DeleteUser("marcos", "123");
        }

        [TestMethod]
        public void Test00CanRegisterAndLoginAnUSer()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");

            Assert.IsTrue(aClient.SameUserNameAndPassword("marcos","123"));
        }

        [TestMethod]
        public void Test000CanNotRegisterADuplicatedUser()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            try
            {
                application.RegisterClient("marcos", "123");
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("User already registered", e.Message);
            }
        }

        [TestMethod]
        public void Test01CanGetAnEmptyCart()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Guid cartId = application.CreateCart(aClient.Id, aClient.Password);

            Cart cart = application.GetCart(cartId);

            Assert.IsTrue(cart.IsEmpty());
        }

        [TestMethod]
        public void Test02CanAddABookInACart()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Guid aCartId = application.CreateCart(aClient.Id, aClient.Password);
            application.AddAQuantityOfAnItem(1, objectProvider.ABook(), aCartId);

            Cart aCart = application.GetCart(aCartId);

            Assert.IsFalse(aCart.IsEmpty());
        }

        [TestMethod]
        public void Test03WhenAddABookInACartThenTheBookIsInTheCart()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            string aBook = objectProvider.ABook();

            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Guid aCartId = application.CreateCart(aClient.Id, aClient.Password);
            application.AddAQuantityOfAnItem(1, aBook, aCartId);

            Cart aCart = application.GetCart(aCartId);

            Assert.IsTrue(aCart.HasABook(aBook));
        }

        [TestMethod]
        public void Test04After30MinutesCanNotAddABookInTheCart()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Guid aCartId = application.CreateCart(aClient.Id, aClient.Password);
            string aBook = objectProvider.ABook();
            string otherBook = objectProvider.OtherBook();

            
            application.AddAQuantityOfAnItem(1, aBook, aCartId);
            application.Clock.UpdateSomeMinutes(30); // minutes
            
            try
            {
                application.AddAQuantityOfAnItem(1, otherBook, aCartId);
                Assert.Fail();
            }
            catch (TimeoutException e)
            {
                Assert.AreEqual("The cart has been expired", e.Message);

                Cart aCart = application.GetCart(aCartId);
                Assert.IsFalse(aCart.HasABook(otherBook));
            }
        }

        [TestMethod]
        public void Test05CanAddABookAfter32MinutesWhenTheLastUsageOfTheCartWasBeforeHisExpiration()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Guid aCartId = application.CreateCart(aClient.Id, aClient.Password);
            string aBook = objectProvider.ABook();

            application.Clock.UpdateSomeMinutes(20); // 20 minutes
            application.AddAQuantityOfAnItem(1, aBook, aCartId);
            
            application.Clock.UpdateSomeMinutes(12); // 12 minutes
            application.AddAQuantityOfAnItem(1, aBook, aCartId);

            Cart aCart = application.GetCart(aCartId);

            Assert.IsTrue(aCart.HasABook(aBook));
        }

        [TestMethod]
        public void Test06CanCheckoutACartWithOneBookWithASpecificCatalog()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Guid aCartId = application.CreateCart(aClient.Id, aClient.Password);

            application.AddAQuantityOfAnItem(1, objectProvider.ABook(), aCartId);

            Guid saleId = application.CheckoutCart(aCartId, objectProvider.AValidCreditCard(), objectProvider.ACatalog());

            Sale sale = application.GetSale(saleId);

            Assert.IsTrue(application.IsSaleRegistered(sale));
        }

        [TestMethod]
        public void Test07CanNotCheckoutACartWithAnInvalidBookWithASpecificCatalog()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Guid aCartId = application.CreateCart(aClient.Id, aClient.Password);

            application.AddAQuantityOfAnItem(1, objectProvider.AnInvalidBook(), aCartId);

            try
            {
                application.CheckoutCart(aCartId, objectProvider.AValidCreditCard(), objectProvider.ACatalog());
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
            
            Assert.IsTrue(application.PurchasesFor(aClient).Item2 == 0);//TODO; ver esta monstruosidad. Asertar lo que corresponda.
        }

        [TestMethod]
        public void Test09WhenAClientHasPurchasesThenHisPurchasesIsNotEmpty()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();

            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");

            Guid aCartId = application.CreateCart(aClient.Id, aClient.Password);

            application.AddAQuantityOfAnItem(1, objectProvider.ABook(), aCartId);
            application.CheckoutCart(aCartId, objectProvider.AValidCreditCard(), objectProvider.ACatalog());

            Assert.IsTrue(application.PurchasesFor(aClient).Item2 != 0);//TODO; ver esta monstruosidad. Asertar lo que corresponda.
        }

        [TestMethod]
        public void Test10WhenAClientBuyABookHisPurchasesHasRegisteredThatSale()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Guid aCartId = application.CreateCart(aClient.Id, aClient.Password);
            application.AddAQuantityOfAnItem(1, objectProvider.ABook(), aCartId);
            Guid saleId = application.CheckoutCart(aCartId, objectProvider.AValidCreditCard(), objectProvider.ACatalog());

            Sale aSale = application.GetSale(saleId);

            Assert.IsTrue(application.PurchasesContainsASaleForAClient(aSale, aClient));
        }

        [TestMethod]
        public void Test11WhenGetListCartOfAnEmptyCartThenIsEmpty()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Guid aCartId = application.CreateCart(aClient.Id, aClient.Password);

            Assert.IsTrue(application.ListCart(aCartId).Count == 0);
        }

        [TestMethod]
        public void Test12WhenAddBookToCartAndGetListCartThenIsNotEmpty()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Guid aCartId = application.CreateCart(aClient.Id, aClient.Password);
            application.AddAQuantityOfAnItem(1, objectProvider.ABook(), aCartId);

            Assert.IsFalse(application.ListCart(aCartId).Count == 0);
        }

        [TestMethod]
        public void Test13WhenAddAQuantityTimesABookToCartAndGetListCartThereAreTheBookWithThisQuantity()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            application.RegisterClient("marcos", "123");
            Client aClient = application.Login("marcos", "123");
            Guid aCartId = application.CreateCart(aClient.Id, aClient.Password);
            String aBook = objectProvider.ABook();
            application.AddAQuantityOfAnItem(4, aBook, aCartId);

            Assert.IsTrue(application.ContainsThisQuantityOfBook(aCartId, aBook, 4));
        }
    }
}
