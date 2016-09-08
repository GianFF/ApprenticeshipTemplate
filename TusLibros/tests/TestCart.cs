using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TusLibros.lib;
using TusLibros.tests.support;

namespace TusLibros
{
    
    [TestClass]
    public class TestCart
    {
        protected Cart Cart;
        private string _anInvalidBook;
        private string _aBook;
        private ObjectProvider _objectProvider;

        [TestInitialize]
        public void SetUp()
        {
            _objectProvider = new ObjectProvider();
            Cart = _objectProvider.EmptyCart();
            _aBook = _objectProvider.ABook();
            _anInvalidBook = _objectProvider.AnInvalidBook();
        }

        [TestMethod]
        public void Test01AtTheBeginTheCartIsEmpty()
        {
            Assert.AreEqual(Cart.TotalItems(), 0);
        }

        [TestMethod]
        public void Test02CanAddBooksToTheCart()
        {
            Cart.AddItem(_aBook);
            Assert.AreEqual(Cart.TotalItems(), 1);
            Assert.IsTrue(Cart.HasABook(_aBook));
        }

        /*
        public void Test03CanNotAddBooksFromOtherEditorialToTheCart()
        {
            try
            {
                Cart.AddItem(_anInvalidBook);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(Cart.ErrorMessageForInvalidBook, e.Message);
            }
        }*/

        [TestMethod]
        public void Test04CanAddMoreThanOnceOfTheSameBookToTheCart()
        {
            Cart.AddItemSomeTimes(_aBook, 2);
            Assert.AreEqual(Cart.TotalItems(), 2);
            Assert.IsTrue(Cart.HasABook(_aBook));
        }
    }
}
