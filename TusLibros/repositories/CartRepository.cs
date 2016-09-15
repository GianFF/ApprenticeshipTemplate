using System;
using NHibernate;
using TusLibros.lib;

namespace TusLibros.repositories
{
    class CartRepository
    {
        public void Add(Cart cart)
        {
            using (ISession session = SessionManager.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(cart);
                transaction.Commit();
            }
        }

        public void Update(Cart cart)
        {
            throw new NotImplementedException();
        }

        public void Remove(Cart cart)
        {
            throw new NotImplementedException();
        }

        public Cart GetById(int cartId)
        {
            throw new NotImplementedException();
        }

        public Cart GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
