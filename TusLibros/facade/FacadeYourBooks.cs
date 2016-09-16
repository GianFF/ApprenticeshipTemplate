using System;
using System.Collections.Generic;
using NHibernate;
using TusLibros.lib;
using TusLibros.repositories;

namespace TusLibros.facade
{
    internal class FacadeYourBooks
    {
        private SystemCart SystemCart;

        private UsersSessionRepository UsersSessionRepository;
        public Clock Clock;
        private List<UsersSession> UsersSessions;

        public FacadeYourBooks()
        {
            Clock = new Clock();
            UsersSessions = new List<UsersSession>();

            SystemCart = new SystemCart();
        }

        public Cart CreateCart()
        {
            Cart aCart = new Cart();
            ISession session = SessionManager.OpenSession();
            ITransaction transaction = session.BeginTransaction();
            SystemCart.Add(aCart, session);
            transaction.Commit();

            return aCart;
        }

        public void AddItem(string aBook, Guid aCartId)
        {
            UsersSession userSession = UsersSessionFor(aCartId);
            userSession.AssertIsCartExpired(Clock.TimeNow());

            Cart aCart = userSession.Cart;

            aCart.AddItem(aBook);
            // TODO: persistir la sesion, que a su vez persiste al carrito.
        }

        private UsersSession UsersSessionFor(Guid aCartId)
        {
            return UsersSessionRepository.GetByCartId(aCartId);
        }
    }
}