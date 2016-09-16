using System;
using NHibernate;
using NHibernate.Criterion;
using TusLibros.lib;
using TusLibros.repositories;

namespace TusLibros.app
{
    internal class PersistentYourBooksApplication : IYourBooksApplication
    {
        public Clock Clock { get; set; }

        public PersistentYourBooksApplication()
        {
            Clock = new Clock(); //TODO: Deberiamos pasarlo por parametro? o quizas una variable de entorno que devuelva el correcto...
        }

        public Cart CreateCart()
        {
            Cart aCart = new Cart();

            ISession session = SessionManager.OpenSession();
            ITransaction transaction = session.BeginTransaction();

            session.Save(new UsersSession(aCart, Clock.TimeNow()), session);

            transaction.Commit();

            return aCart;
        }

        public void AddItem(string aBook, Guid aCartId)
        {
            ISession session = SessionManager.OpenSession();
            ITransaction transaction = session.BeginTransaction();

            UsersSession userSession = session
                .CreateCriteria(typeof(UsersSession))
                .Add(Restrictions.Eq("CartId", aCartId))
                .UniqueResult<UsersSession>();

            userSession.VerifyCartExpired(Clock.TimeNow());
            Cart aCart = userSession.Cart;
            aCart.AddItem(aBook); 
            session.Update(userSession, session);

            transaction.Commit();
        }
    }
}