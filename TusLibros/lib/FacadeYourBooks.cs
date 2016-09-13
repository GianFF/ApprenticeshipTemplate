using System;
using System.Collections.Generic;

namespace TusLibros.lib
{
    internal class FacadeYourBooks
    {
        protected List<String> RegisterUsers;

        public FacadeYourBooks()
        {
            RegisterUsers = new List<string>();
        }

        public void Register(String anUser)
        {
            RegisterUsers.Add(anUser);
        }

        public bool IsRegistered(string anUser)
        {
            return ExistsUser(anUser);
        }

        public Cart Loggin(String anUser)
        {
            VerifyThatExistsUser(anUser);
            return CartFor(anUser);
        }

        private Cart CartFor(string anUser)
        {
            return new Cart();
        }

        private void VerifyThatExistsUser(string anUser)
        {
            if (!ExistsUser(anUser))
            {
                throw new SystemException("Not registered user");
            }
        }

        private bool ExistsUser(string anUser)
        {
            return RegisterUsers.Contains(anUser);
        }

    }
}