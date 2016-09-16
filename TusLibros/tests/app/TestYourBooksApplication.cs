using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TusLibros.app;
using TusLibros.lib;
using TusLibros.tests.support;

namespace TusLibros.tests.app
{
    [TestClass]
    public class TestYourBooksApplication
    {
        private ObjectProvider objectProvider;

        [TestInitialize]
        public void SetUp()
        {
            objectProvider = new ObjectProvider();
        }

        [TestMethod]
        public void Test01CanGetAnEmptyCart()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            Cart cart = application.CreateCart();

            Assert.IsTrue(cart.IsEmpty());
        }

        [TestMethod]
        public void Test02CanAddABookInACart()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            Cart aCart = application.CreateCart();

            application.AddItem(objectProvider.ABook(), aCart.Id);

            Assert.IsFalse(aCart.IsEmpty());
        }

        [TestMethod]
        public void Test03WhenAddABookInACartThenTheBookIsInTheCart()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            string aBook = objectProvider.ABook();

            Cart aCart = application.CreateCart();

            application.AddItem(objectProvider.ABook(), aCart.Id);

            Assert.IsTrue(aCart.HasABook(aBook));
        }

        [TestMethod]
        public void Test04After30MinutesCanNotAddABookInTheCart()
        {
            IYourBooksApplication application = objectProvider.YourBooksApplication();
            Cart aCart = application.CreateCart();
            string aBook = objectProvider.ABook();

            application.AddItem(objectProvider.ABook(), aCart.Id);
            application.Clock.UpdateSomeMinutes(30); // minutes
            
            try
            {
                application.AddItem(aBook, aCart.Id);
                Assert.Fail();
            }
            catch (TimeoutException e)
            {
                Assert.AreEqual("The cart has been expired", e.Message);
            }

        }
    }
}
