using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TusLibros.lib;
using TusLibros.tests.support;

namespace TusLibros.tests
{
    [TestClass]
    public class TestFacadeYourBooks
    {
        private ObjectProvider objectProvider;
        private FacadeYourBooks facade;
        private string anUser;

        [TestInitialize]
        public void SetUp()
        {
            objectProvider = new ObjectProvider();
            facade = objectProvider.AFacade();
            anUser = objectProvider.AValidUser();
        }

        [TestMethod]
        public void Test01CanRegisterAUser()
        {
            facade.Register(anUser);

            Assert.IsTrue(facade.IsRegistered(anUser));
        }

        [TestMethod]
        public void Test02CanNotLogginAnInvalidUser()
        {
            try
            {
                facade.Loggin(anUser);
                Assert.Fail();
            }
            catch (SystemException e)
            {
                Assert.AreEqual("Not registered user", e.Message);
            }
        }

        [TestMethod]
        public void Test03WhenAUserLogginThenObtainsACart()
        {
            facade.Register(anUser);
            Cart userCart = facade.Loggin(anUser);

            Assert.AreEqual(objectProvider.EmptyCart(), userCart);
        }

        [TestMethod]
        public void Test04AUserThanDoNotBuyAnthingHasAnEmptyCart()
        {
            facade.Register(anUser);
            Cart userCart = facade.Loggin(anUser);

            Assert.IsTrue(userCart.IsEmpty());
        }
    }
}
