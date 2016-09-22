using System;
using System.Collections;

namespace TusLibros.model.entities
{
    public class Sale
    {
        //TODO: la venta no tiene una fecha?
        public virtual Guid Id { get; protected set; }
        public virtual Client Client { get; set; }
        public virtual CreditCard CreditCard { get; protected set; }
        public virtual Hashtable BooksAndPrices { get; protected set; }

        public Sale()
        {
            
        }

        public Sale(CreditCard aCreditCard, Hashtable booksAndPrices, Client aClient)
        {
            CreditCard = aCreditCard;
            BooksAndPrices = booksAndPrices;
            Client = aClient;
        }

        public bool ForClient(Client aClient)
        {
            return Client.Equals(aClient);
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