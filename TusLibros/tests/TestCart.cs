using Microsoft.VisualStudio.TestTools.UnitTesting;
using TusLibros.model.entities;
using TusLibros.tests.support;

namespace TusLibros.tests
{
    [TestClass]
    public class TestCart
    {
        protected Cart cart;
        private string aBook;
        private string otherBook;
        private TestObjectProvider objectProvider;

        [TestInitialize]
        public void SetUp()
        {
            objectProvider = new TestObjectProvider();
            cart = objectProvider.EmptyCart();
            aBook = objectProvider.ABook();
            otherBook = objectProvider.OtherBook();
        }

        [TestMethod]
        public void Test01AtTheBeginTheCartIsEmpty()
        {
            Assert.IsTrue(cart.IsEmpty());
        }

        [TestMethod]
        public void Test02WhenAddBooksToTheCartThisNotIsEmpty()
        {
            cart.AddItem(aBook);
            Assert.IsFalse(cart.IsEmpty());
        }

        [TestMethod]
        public void Test03WhitOneBookHasOnlyTheBookThatAdd()
        {
            cart.AddItem(aBook);
            Assert.IsTrue(cart.HasABook(aBook));
        }

        [TestMethod]
        public void Test04CanAddMoreThanOnceOfTheSameBookToTheCart()
        {
            cart.AddItemSomeTimes(aBook, 2);
            Assert.AreEqual(cart.TotalItems(), 2);
            Assert.IsTrue(cart.HasABook(aBook));
        }

        [TestMethod]
        public void Test05WhenConsultForABookThatIsNotAddedReturnsFalse()
        {
            cart.AddItem(aBook);
            Assert.IsTrue(cart.HasABook(aBook));
            Assert.IsFalse(cart.HasABook(otherBook));
        }

        [TestMethod]
        public void Test06WhenAddSeveralTimesABookReturnTheSameOcurrencesOfThisBook()
        {
            cart.AddItemSomeTimes(aBook, 4);
            Assert.AreEqual(cart.QuantityOf(aBook), 4);
        }
    }
}



