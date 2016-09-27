using System;
using System.Collections;

namespace TusLibros.model.entities
{
    public class Sale
    {
        public virtual Guid Id { get; protected set; }
        public virtual DateTime Date { get; set; }
        public virtual Client Client { get; set; }
        public virtual CreditCard CreditCard { get; set; }
        public virtual IDictionary BooksAndPrices { get; set; }

        public Sale() { }

        public Sale(CreditCard aCreditCard, IDictionary booksAndPrices, Client aClient, DateTime aDate)
        {
            Date = aDate;
            CreditCard = aCreditCard;
            BooksAndPrices = booksAndPrices;
            Client = aClient;
        }

        public virtual bool ForClient(Client aClient)
        {
            return Client.SameUserNameAndPassword(aClient.UserName, aClient.Password);
        }
    }
}