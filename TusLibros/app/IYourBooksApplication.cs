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
        Cart CreateCart(Guid clientId, String password);
        Cart AddAQuantityOfAnItem(int quantity, string aBook, Guid aCartId);
        Cart GetCart(Guid aCartId);
        List<Sale> PurchasesFor(Client aClient);
        Sale CheckoutCart(Guid aCartId, CreditCard aCreditCard, IDictionary aCatalog);
        bool IsRegistered(Sale sale);
        bool PurchasesContainsASaleForAClient(Sale aSale, Client aClient);
        IDictionary ListCart(Guid aCartId);
        bool ContainsThisQuantityOfBook(Guid aCartId, string aBook, int quantity);
        bool CanHandle(String environment);
        Client Login(string userName, string password);
        void RegisterClient(string userName, string password);
        void DeleteUser(string userName, string password);
    }
}
