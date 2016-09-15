using System;
using TusLibros.lib;

namespace TusLibros.facade
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

        public void AssertIsCartExpired(DateTime timeNow)
        {
            if ((timeNow.Subtract(Date)).TotalMinutes >= 30)
                throw new TimeoutException("The cart has been expired");
        }
    }
}