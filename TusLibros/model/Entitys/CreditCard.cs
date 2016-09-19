using System;

namespace TusLibros.model.Entitys
{
    public class CreditCard
    {
        public virtual Guid Id { get; protected set; }
        public virtual DateTime ExpirationDate { get; protected set; }

        public CreditCard(DateTime aDate)
        {
            ExpirationDate = aDate;
        }

        public bool IsExpired()
        {
            return ExpirationDate < DateTime.Now;
        }
    }
}
