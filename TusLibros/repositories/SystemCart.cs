using System;
using NHibernate;
using TusLibros.lib;

namespace TusLibros.repositories
{
    class SystemCart // en realidad es: PersistentSystemCart
    {
        public void Add(Cart cart, ISession session)
        {
            session.Save(cart);
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
