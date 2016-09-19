using System;
using TusLibros.model;

namespace TusLibros.app
{
    internal class UsersSession
    {
        public virtual Guid Id { get; protected set; }
        public virtual Cart Cart { get; protected set; }
        public virtual DateTime Date { get; protected set; }

        public UsersSession(Cart cart, DateTime date)
        {
            this.Cart = cart;
            this.Date = date;
        }

        public void VerifyCartExpired(DateTime timeNow)
        {
            if ((timeNow.Subtract(Date)).TotalMinutes >= 30)
                throw new TimeoutException("The cart has been expired");
        }
    }
}