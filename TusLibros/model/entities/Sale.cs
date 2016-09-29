using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TusLibros.model.entities
{
    public class Sale
    {
        public virtual Guid Id { get; protected set; }
        public virtual DateTime Date { get; set; }
        public virtual Client Client { get; set; }
        public virtual CreditCard CreditCard { get; set; }
        public virtual List<SaleDetail> SaleDetails { get; set; }
        public virtual Guid TransactionId { get; set; }

        public Sale() { }

        public Sale(CreditCard aCreditCard, List<SaleDetail> aSaleDetail, Client aClient, DateTime aDate)
        {
            Date = aDate;
            Client = aClient;
            CreditCard = aCreditCard;
            SaleDetails = aSaleDetail;
            TransactionId = Guid.NewGuid();
        }

        public virtual bool ForClient(Client aClient)
        {
            return Client.SameUserNameAndPassword(aClient.UserName, aClient.Password);
        }

        public IDictionary BooksAndQuantitys()
        {
            var booksAndQuantitys = new Dictionary<string, int>();
            SaleDetails.ForEach(aSaleDetail => booksAndQuantitys.Add(aSaleDetail.Book, aSaleDetail.Quantity));
            return booksAndQuantitys;
        }


        public int Total()
        {
            return SaleDetails.Sum(aSaleDetail => aSaleDetail.Price);
        }

        public void AddBooksWithOcurrencies(Dictionary<string, int> listBooksWithOccurrences)
        {
            SaleDetails.ForEach(aDetail => aDetail.AddBookWithOcurrencie(listBooksWithOccurrences));
        }
    }
}




