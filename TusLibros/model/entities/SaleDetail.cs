using System;
using System.Collections.Generic;

namespace TusLibros.model.entities
{
    public class SaleDetail
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Book { get; set; }
        public virtual int Quantity { get; set; }
        public virtual int Price { get; set; }

        public SaleDetail(){}

        public SaleDetail(string aBook, int aQuantity, int aPrice)
        {
            Book = aBook;
            Quantity = aQuantity;
            Price = aPrice;
        }

        public virtual void AddBookWithOcurrencie(Dictionary<string, int> listBooksWithOccurrences)
        {
            if (listBooksWithOccurrences.ContainsKey(Book))
            {
                listBooksWithOccurrences[Book] += Quantity;
                return;
            }
            listBooksWithOccurrences.Add(Book, Quantity);
        }
    }
}