using System;

namespace TusLibros.model.entities
{
    public class CreditCard
    {
        public virtual Guid Id { get; protected set; }
        public virtual DateTime ExpirationDate { get; protected set; }
        public virtual int CardNumber { get; protected set; }

        public CreditCard() { }

        public CreditCard(DateTime expirationDate, int cardNumber)
        {
            ExpirationDate = expirationDate;
            CardNumber = cardNumber;
        }

        public virtual bool IsExpired()
        {
            return ExpirationDate < DateTime.Now;
        }
    }
}
