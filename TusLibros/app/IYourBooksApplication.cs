using System;
using System.Collections;
using System.Collections.Generic;
using TusLibros.clocks;
using TusLibros.model.entities;

namespace TusLibros.app
{
    public interface IYourBooksApplication
    {
        Guid CreateCart(Guid clientId, String password);
        void AddAQuantityOfAnItem(int quantity, string aBook, Guid aCartId);
        IDictionary<string, int> ListCart(Guid aCartId);
        Guid CheckoutCart(Guid aCartId, CreditCard aCreditCard, IDictionary<string, int> aCatalog);
        Tuple<IDictionary, int> PurchasesFor(Client aClient);

        Client Login(string userName, string password);
        void RegisterClient(string userName, string password);

        Cart GetCart(Guid aCartId);
        bool IsSaleRegistered(Sale sale);
        bool PurchasesContainsASaleForAClient(Sale aSale, Client aClient);
        bool ContainsThisQuantityOfBook(Guid aCartId, string aBook, int quantity);       
        void DeleteUser(string userName, string password);
        Sale GetSale(Guid transactionId);
        IClock Clock { get; set; }
    }
}
