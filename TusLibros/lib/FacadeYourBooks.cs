using System;
using System.Collections.Generic;

namespace TusLibros.lib
{
    internal class FacadeYourBooks
    {
        public Cart CartFor(string anUser)
        {
            return new Cart();
        }
    }
}