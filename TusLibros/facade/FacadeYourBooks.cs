using System;
using System.Collections.Generic;
using TusLibros.lib;

namespace TusLibros.facade
{
    internal class FacadeYourBooks
    {
        public Clock clock;
        private List<UsersSession> UsersSessions;

        public FacadeYourBooks()
        {
            clock = new Clock();
        }

        public Cart CreateCart()
        {
            Cart aCart = new Cart();
            UsersSessions.Add(new UsersSession(aCart, clock.TimeNow()));
            // TODO: persistir carrito
            // TODO: persistir sesion
            return aCart;
        }

        public void AddItem(string aBook, Cart aCart)
        {
            // TODO: agarrar carrito de la BD
            UsersSessionFor(aCart).AssertIsCartExpired(clock.TimeNow());
            aCart.AddItem(aBook);
            // TODO: persistir carrito
        }

        private UsersSession UsersSessionFor(Cart aCart)
        {
            // TODO: agarrar sesion de la BD
            throw new NotImplementedException();
        }
    }
}