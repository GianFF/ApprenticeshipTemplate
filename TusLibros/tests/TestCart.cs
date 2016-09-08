using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TusLibros
{
    
    [TestClass]
    public class TestCart
    {
        protected Cart cart;
        private string an_invalid_book;
        private string a_book;
        private ObjectProvider objectProvider;

        [TestInitialize]
        public void setUp()
        {
            objectProvider = new ObjectProvider();
            cart = objectProvider.emptyCart();
            a_book = objectProvider.aBook();
            an_invalid_book = objectProvider.anInvalidBook();
        }

        [TestMethod]
        public void test01AtTheBeginTheCartIsEmpty()
        {
            Assert.AreEqual(cart.totalItems(), 0);
        }

        [TestMethod]
        public void test02CanAddBooksToTheCart()
        {
            cart.addItem(a_book);
            Assert.AreEqual(cart.totalItems(), 1);
            Assert.IsTrue(cart.hasABook(a_book));
        }

        [TestMethod]
        public void test03CanNotAddBooksFromOtherEditorialToTheCart()
        {
            try
            {
                cart.addItem(an_invalid_book);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(Cart.ERROR_MESSAGE_FOR_INVALID_BOOK, e.Message);
            }
        }

        [TestMethod]
        public void test04CanAddMoreThanOnceOfTheSameBookToTheCart()
        {
            cart.addItemSomeTimes(a_book, 2);
            Assert.AreEqual(cart.totalItems(), 2);
            Assert.IsTrue(cart.hasABook(a_book));
        }
    }
}
