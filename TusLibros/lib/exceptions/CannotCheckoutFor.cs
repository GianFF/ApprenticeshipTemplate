using System;
using System.Runtime.Serialization;

namespace TusLibros.lib
{
    [Serializable]
    internal class CannotCheckoutFor : Exception
    {
        public CannotCheckoutFor()
        {
        }

        public CannotCheckoutFor(string message) : base(message)
        {
        }

        public CannotCheckoutFor(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotCheckoutFor(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}