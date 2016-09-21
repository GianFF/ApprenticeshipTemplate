using System;
using System.Collections;
using System.Collections.Generic;
using TusLibros.clocks;
using TusLibros.model.entities;

namespace TusLibros.app
{
    public interface IYourBooksApplication
    {
        IClock Clock { get; set; }
        Cart CreateCart();
        void AddAQuantityOfAnItem(int quantity, string aBook, Guid aCartId);
        Cart GetCart(Guid aCartId);
        List<Sale> PurchasesFor(Hashtable aClient);
        Sale CheckoutCart(Guid aCartId, CreditCard aCreditCard, Hashtable aCatalog, Hashtable aClient);
        bool IsRegistered(Sale sale);
    }
}
