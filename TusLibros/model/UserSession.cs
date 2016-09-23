using System;
using TusLibros.model.entities;

namespace TusLibros.model
{
    public class UserSession
    {
        public virtual Guid Id { get; protected set; }
        public virtual Cart Cart { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual Client Client { get; set; }

        public UserSession(){}

        public UserSession(Cart cart, DateTime date, Client client)
        {
            this.Cart = cart;
            this.Date = date;
            this.Client = client;
        }

        public virtual void VerifyCartExpired(DateTime timeNow)
        {
            if ((timeNow.Subtract(Date)).TotalMinutes >= 30)
                throw new TimeoutException("The cart has been expired");
        }
    }
}