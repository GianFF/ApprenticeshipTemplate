using System;
using System.Collections.Generic;

namespace TusLibros.lib
{
    internal class FacadeYourBooks
    {
        protected List<String> LoginUsers;
        public FacadeYourBooks()
        {
            LoginUsers = new List<string>();
        }

        public void Register(String anUser)
        {
            LoginUsers.Add(anUser);
        }

        public Cart Loguin(String anUser)
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
                throw new NotImplementedException();
            }
        }

        private bool ExistsUser(string anUser)
        {
            return LoginUsers.Contains(anUser);
        }

        public List<Cart> BuysFor(String aClient)
        {
            return new List<Cart>();
        }
    }
}