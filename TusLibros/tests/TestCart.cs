using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TusLibros.lib;
using TusLibros.tests.support;

namespace TusLibros.tests
{
    [TestClass]
    public class TestCart
    {
        protected Cart cart;
        private string anInvalidBook;
        private string aBook;
        private string otherBook;
        private ObjectProvider objectProvider;

        [TestInitialize]
        public void SetUp()
        {
            objectProvider = new ObjectProvider();
            cart = objectProvider.EmptyCart();
            aBook = objectProvider.ABook();
            otherBook = objectProvider.OtherBook();
            anInvalidBook = objectProvider.AnInvalidBook();
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


/* context 'con un libro de otra editorial' do
    let(:libro_invalido) {proveedor_de_objetos.un_libro_invalido}
    it 'retorna error al agregarlo' do
      expect{carrito.agregar_libro(libro_invalido)}.
          to raise_error ArgumentError,Carrito.mensaje_error_libro_de_otra_editorial
    end
  end

end
----
    public void Test03CanNotAddBooksFromOtherEditorialToTheCart()
    {
        try
        {
            cart.AddItem(anInvalidBook);
            Assert.Fail();
        }
        catch (Exception e)
        {
            Assert.AreEqual(cart.ErrorMessageForInvalidBook, e.Message);
        }
    }*/



