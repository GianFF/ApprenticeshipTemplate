using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate.Util;

namespace TusLibros.model.entities
{
    public class MerchantProcessor
    {
        public virtual Guid Id { get; protected set; }
        public virtual IDictionary SuccessfulOperations { get; protected set; }

        public MerchantProcessor()
        {
            SuccessfulOperations = new Dictionary<CreditCard,int>();    
        }

        public void RegisterTransaction(CreditCard aCreditCard, int anAmount)
        {
            SuccessfulOperations.Add(aCreditCard, anAmount);
        }
    }
}
