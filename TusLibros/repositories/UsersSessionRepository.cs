using System;
using NHibernate;
using NHibernate.Criterion;
using TusLibros.facade;

namespace TusLibros.repositories
{
    internal class UsersSessionRepository
    {
        public void Add(UsersSession userSession)
        {
            using (ISession session = SessionManager.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(userSession);
                transaction.Commit();
            }
        }

        public UsersSession GetById(Guid usersSessionId)
        {
            using (ISession session = SessionManager.OpenSession())
                return session.Get<UsersSession>(usersSessionId);
        }

        public UsersSession GetByCartId(Guid cartId)
        {
            using (ISession session = SessionManager.OpenSession())
            {
                UsersSession usersSession = session
                    .CreateCriteria(typeof(UsersSession))
                    .Add(Restrictions.Eq("Id", cartId))
                    .UniqueResult<UsersSession>();
                return usersSession;
            }
        }
    }
}