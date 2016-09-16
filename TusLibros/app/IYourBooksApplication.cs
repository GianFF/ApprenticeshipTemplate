using System;
using TusLibros.clocks;
using TusLibros.lib;

namespace TusLibros.app
{
    public interface IYourBooksApplication
    {
        IClock Clock { get; set; }
        Cart CreateCart();
        void AddAQuantityOfAnItem(int quantity, string aBook, Guid aCartId);

    }
}
