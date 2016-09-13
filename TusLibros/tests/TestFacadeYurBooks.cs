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
        public void Test01CanGetAnEmptyCartForAUser()
        {
            Cart cart = facade.CartFor(anUser);

            Assert.IsTrue(cart.IsEmpty());
        }
    }
}
