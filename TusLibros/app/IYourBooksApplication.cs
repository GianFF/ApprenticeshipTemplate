using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TusLibros.lib;

namespace TusLibros.app
{
    public interface IYourBooksApplication
    {
        Clock Clock { get; set; }
        Cart CreateCart();
        void AddItem(string aBook, Guid aCartId);

    }
}
