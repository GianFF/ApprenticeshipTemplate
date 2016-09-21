using System;
using System.Collections;
using NHibernate.Util;

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

        public void RegisterTransaction(CreditCard aCreditCard, int anAmount)
        {
            SuccessfulOperations.Add(aCreditCard, anAmount);
        }
    }
}
