using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TusLibros.lib;

namespace TusLibros{
    class ObjectProvider
    {
        public String anInvalidBook()
        {
            return "Book from other editorial";
        }

        public String aBook()
        {
            return "nacidos de la bruma";
        }

        public Cart emptyCart()
        {
            return new Cart();
        }
        
        
        // NO USADOS
        public String otherBook()
        {
            return "nacidos de la bruma el imperio final";
        }

        public Cart aCartWithOneBook()
        {
            Cart cart = this.emptyCart();
            cart.addItem(this.aBook());
            return cart;
        }

        public Cart aCartWithTwoBooks()
        {
            Cart cart = new TusLibros.Cart();
            cart.addItemSomeTimes(this.aBook(), 2);
            return cart;
        }

        public Cashier aCashier()
        {
            return new Cashier();
        }


    }
}
