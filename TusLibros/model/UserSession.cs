using System;
using System.Collections;
using System.Collections.Generic;
using TusLibros.app;
using TusLibros.clocks;
using TusLibros.model.entities;

namespace TusLibros.model
{
    public class UserSession
    {
        public virtual Guid Id { get; protected set; }
        public virtual Guid CartId { get; protected set; }
        public virtual Cart Cart { get; set; }
        public virtual DateTime LastActionDateTime { get; set; }
        public virtual Client Client { get; set; }

        public UserSession(){}

        public UserSession(DateTime lastActionDateTime, Client client)
        {
            this.Cart = new Cart();
            this.LastActionDateTime = lastActionDateTime;
            this.Client = client;
            this.CartId = Guid.NewGuid();
        }

        public virtual void VerifyCartExpired(DateTime timeNow)
        {
            if ((timeNow.Subtract(LastActionDateTime)).TotalMinutes >= 30)
                throw new TimeoutException("The cart has been expired");
        }

        public virtual void UpdateLastActionTime(DateTime lastActionDateTime)
        {
            LastActionDateTime = lastActionDateTime;
        }

        public void AddQuantityOfAnItem(string aBook, int quantity, IClock aClock)
        {
            Cart.AddItemSomeTimes(aBook,quantity);
            UpdateLastActionTime(aClock.TimeNow());
        }

        public IDictionary<string,int> ListCart()
        {
            return Cart.Items;
        }

        public Sale CheckoutCartWith(CreditCard aCreditCard, MerchantProcessor merchantProcessor, IDictionary<string, int> aCatalog)
        {
            Cashier aCashier = new Cashier();
            Sale aSale = aCashier.CheckoutFor(aCreditCard, Cart, aCatalog, Client, merchantProcessor);
            return aSale;
        }
    }
}