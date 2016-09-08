using System;
using System.Collections;

namespace TusLibros.lib
{
    public class MerchantProcessor
    {
        protected Hashtable SuccessfulOperations;

        public MerchantProcessor()
        {
            SuccessfulOperations = new Hashtable();    
        }

        public void RegisterTransaction(CreditCard aCreditCard, int priceFor)
        {
            SuccessfulOperations.Add(aCreditCard,priceFor);
        }

        public int LastSuccessfulOperationFor(CreditCard aCreditCard)
        {
            return (int) SuccessfulOperations[aCreditCard];
        }
    }
}
