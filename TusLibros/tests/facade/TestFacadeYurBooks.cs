using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TusLibros.facade;
using TusLibros.lib;
using TusLibros.tests.support;

namespace TusLibros.tests.facade
{
    [TestClass]
    public class TestFacadeYourBooks
    {
        private ObjectProvider objectProvider;
        private FacadeYourBooks facade;
        private string anUser;
        private string aBook;
        private Clock clock;


        [TestInitialize]
        public void SetUp()
        {
            objectProvider = new ObjectProvider();
            facade = objectProvider.AFacade();
            anUser = objectProvider.AValidUser();
            aBook = objectProvider.ABook();
            clock = objectProvider.AClock();
        }

        [TestMethod]
        public void Test01CanGetAnEmptyCart()
        {
            Cart cart = facade.CreateCart();

            Assert.IsTrue(cart.IsEmpty());
        }

        [TestMethod]
        public void Test02CanAddABookInACart()
        {
            Cart aCart = facade.CreateCart();

            facade.AddItem(aBook, aCart.Id);

            Assert.IsFalse(aCart.IsEmpty());
        }

        [TestMethod]
        public void Test03WhenAddABookInACartThenTheBookIsInTheCart()
        {
            Cart aCart = facade.CreateCart();

            facade.AddItem(aBook, aCart.Id);

            Assert.IsTrue(aCart.HasABook(aBook));
        }

        [TestMethod]
        public void Test04After30MinutesCanNotAddABookInTheCart()
        {
            Cart aCart = facade.CreateCart();

            facade.AddItem(aBook, aCart);
            clock.UpdateSomeMinutes(30); // minutes
            
            try
            {
                facade.AddItem(aBook, aCart);
                Assert.Fail();
            }
            catch (TimeoutException e)
            {
                Assert.AreEqual("The cart has been expired", e.Message);
            }

        }
    }
}
