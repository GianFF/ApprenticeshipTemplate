using System;
using TusLibros.model.entities;

namespace TusLibros.model
{
    public class UserSession
    {
        public virtual Guid Id { get; protected set; }
        public virtual Cart Cart { get; set; }
        public virtual DateTime LastActionDateTime { get; set; }
        public virtual Client Client { get; set; }

        public UserSession(){}

        public UserSession(Cart cart, DateTime lastActionDateTime, Client client)
        {
            this.Cart = cart;
            this.LastActionDateTime = lastActionDateTime;
            this.Client = client;
            //generar id
            //this.CartId = 1;
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
    }
}