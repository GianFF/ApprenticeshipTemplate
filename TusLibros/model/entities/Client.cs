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

        public virtual bool SameUserName(string userName)
        {
            return UserName == userName;
        }

        public virtual bool SameUserNameAndPassword(string userName, string password)
        {
            return SameUserName(userName) && Password == password;
        }
    }
}
