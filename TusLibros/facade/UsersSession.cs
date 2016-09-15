using System;
using TusLibros.lib;

namespace TusLibros.facade
{
    internal class UsersSession
    {
        private Cart aCart;
        private DateTime date;

        public UsersSession(Cart aCart, DateTime date)
        {
            this.aCart = aCart;
            this.date = date;
        }

        public void AssertIsCartExpired(DateTime timeNow)
        {
            if ((timeNow.Subtract(date)).TotalMinutes >= 30)
                throw new TimeoutException("The cart has been expired");
        }
    }
}