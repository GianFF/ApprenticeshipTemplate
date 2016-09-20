using System;
using System.Collections.Generic;
using TusLibros.clocks;
using TusLibros.model;
using TusLibros.model.entities;

namespace TusLibros.app
{
    internal class TransientPersistentYourBooksApplication : IYourBooksApplication
    {
        public IClock Clock { get; set; }
        public List<UserSession> UserSessions { get; set; }

        public TransientPersistentYourBooksApplication()
        {
            UserSessions = new List<UserSession>();
            Clock = new DevelopmentClock();
        }

        public Cart CreateCart()
        {
            Cart aCart = new Cart();
            UserSessions.Add(new UserSession(aCart, Clock.TimeNow()));
            return aCart;
        }

        public void AddAQuantityOfAnItem(int quantity, string aBook, Guid aCartId)
        {
            UserSession userSession = UserSession(aCartId);
            userSession.VerifyCartExpired(Clock.TimeNow());
            Cart aCart = userSession.Cart;
            aCart.AddItemSomeTimes(aBook, quantity);
        }

        public Cart GetCart(Guid aCartId)
        {
            UserSession userSession = UserSession(aCartId);
            return userSession.Cart;
        }

        private UserSession UserSession(Guid aCartId)
        {
            return UserSessions.Find(session => session.Cart.Id == aCartId);
        }
    }
}