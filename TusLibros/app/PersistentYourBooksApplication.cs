using System;
using NHibernate;
using NHibernate.Criterion;
using TusLibros.clocks;
using TusLibros.db;
using TusLibros.model;
using TusLibros.model.entities;

namespace TusLibros.app
{
    internal class PersistentYourBooksApplication : IYourBooksApplication
    {
        public IClock Clock { get; set; }

        public PersistentYourBooksApplication()
        {
            Clock = new IntegrationClock();
        }

        public Cart CreateCart()
        {
            Cart aCart = new Cart();

            ISession session = SessionManager.OpenSession();
            ITransaction transaction = session.BeginTransaction();

            session.SaveOrUpdate(new UserSession(aCart, Clock.TimeNow()));

            transaction.Commit();

            return aCart;
        }

        public void AddAQuantityOfAnItem(int quantity, string aBook, Guid aCartId)
        {
            ISession session = SessionManager.OpenSession();
            ITransaction transaction = session.BeginTransaction();

            UserSession userSession = session.QueryOver<UserSession>().Where(uS => uS.Cart.Id == aCartId).SingleOrDefault<UserSession>();

            userSession.VerifyCartExpired(Clock.TimeNow());
            Cart aCart = userSession.Cart;
            aCart.AddItemSomeTimes(aBook, quantity);
            session.SaveOrUpdate(userSession);

            transaction.Commit();
        }

        public Cart GetCart(Guid aCartId)
        {
            ISession session = SessionManager.OpenSession();

            Cart cart = session.Get<Cart>(aCartId);

            return cart;
        }
    }
}