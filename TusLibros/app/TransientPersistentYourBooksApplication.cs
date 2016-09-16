﻿using System;
using System.Collections.Generic;
using TusLibros.clocks;
using TusLibros.lib;

namespace TusLibros.app
{
    internal class TransientPersistentYourBooksApplication : IYourBooksApplication
    {
        public IClock Clock { get; set; }
        public List<UsersSession> UserSessions { get; set; }

        public TransientPersistentYourBooksApplication()
        {
            UserSessions = new List<UsersSession>();
            Clock = new Clock(); //TODO: Deberiamos pasarlo por parametro? o quizas una variable de entorno que devuelva el correcto...
        }

        public Cart CreateCart()
        {
            Cart aCart = new Cart();
            UserSessions.Add(new UsersSession(aCart, Clock.TimeNow()));
            return aCart;
        }

        public void AddItem(string aBook, Guid aCartId)
        {
            UsersSession userSession = UserSessions.Find(session => session.Cart.Id == aCartId);
            userSession.VerifyCartExpired(Clock.TimeNow());
            Cart aCart = userSession.Cart;
            aCart.AddItem(aBook);
        }
    }
}