using System;

namespace TusLibros.app
{
    public class RegisterException : Exception
    {
        public RegisterException(string message) : base(message)
        {
        }
    }
}