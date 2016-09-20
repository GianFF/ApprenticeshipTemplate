using System;
using System.Collections;

namespace TusLibros.model.entities
{
    public class MerchantProcessor
    {
        public virtual Guid Id { get; protected set; }
        public virtual Hashtable SuccessfulOperations { get; protected set; }

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
