using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TusLibros.model.entities
{
    public class Client
    {
        public virtual Guid Id { get; protected set; }
        public virtual CreditCard CreditCard { get; protected set; }

        public virtual String UserName { get; protected set; }

        public virtual String Password { get; protected set; }

        public Client(CreditCard aCreditCard, String anUserNAme, String aPassword)
        {
            CreditCard = aCreditCard;
            UserName = anUserNAme;
            Password = aPassword;
        }

        public override bool Equals(Object otherClient)
        {
            Client aClient = otherClient as Client;
            if ((Object)aClient == null)
            {
                return false;
            }

            return UserName == aClient.UserName && Password == aClient.Password;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
