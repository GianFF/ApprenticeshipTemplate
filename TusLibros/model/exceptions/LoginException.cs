using System;

namespace TusLibros.model.exceptions
{
    public class LoginException : Exception
    {
        public LoginException(string message) : base(message)
        {
        }
    }
}