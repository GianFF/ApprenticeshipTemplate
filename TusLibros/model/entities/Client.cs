using System;

namespace TusLibros.model.entities
{
    public class Client
    {
        public virtual Guid Id { get; protected set; }

        public virtual String UserName { get; set; }

        public virtual String Password { get; set; }


        public Client() { }

        public Client(String anUserNAme, String aPassword)
        {         
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
