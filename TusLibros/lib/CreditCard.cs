using System;

namespace TusLibros.lib
{
    public class CreditCard
    {
        public DateTime ExpirationDate;
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
