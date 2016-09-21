using System;
using System.Collections;

namespace TusLibros.model.entities
{
    public class Sale
    {
        //TODO: la venta no tiene una fecha?
        public virtual Guid Id { get; protected set; }
        public Hashtable Client { get; set; }
        private CreditCard CreditCard;
        private Hashtable CatalogSubset;

        public Sale(CreditCard aCreditCard, Hashtable catalogSubset, Hashtable aClient)
        {
            CreditCard = aCreditCard;
            CatalogSubset = catalogSubset;
            Client = aClient;
        }

        public bool ForClient(Hashtable aClient)
        {
            return Client == aClient;
        }

        public bool Equals(Sale sale)
        {
            return this.CreditCard == sale.CreditCard; // TODO: && fecha && mas cosas
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Sale);
        }
    }
}