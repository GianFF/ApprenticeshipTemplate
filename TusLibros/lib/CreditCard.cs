using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
