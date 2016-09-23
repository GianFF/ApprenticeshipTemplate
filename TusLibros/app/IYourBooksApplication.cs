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
        Cart AddAQuantityOfAnItem(int quantity, string aBook, Guid aCartId);
        Cart GetCart(Guid aCartId);
        List<Sale> PurchasesFor(Client aClient);
        Sale CheckoutCart(Guid aCartId, CreditCard aCreditCard, IDictionary aCatalog, Client aClient);
        bool IsRegistered(Sale sale);
        bool PurchasesContainsFor(Sale aSale, Client aClient);
        IDictionary ListCart(Guid aCartId);
        bool ContainsThisQuantityOfBook(Guid aCartId, string aBook, int quantity);
        bool CanHandle(String environment);
    }
}
