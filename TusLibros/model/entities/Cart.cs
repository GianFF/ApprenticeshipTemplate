using System;
using System.Collections;
using System.Collections.Generic;
using FluentNHibernate.Conventions;
using NHibernate.Util;

namespace TusLibros.model.entities
{

    public class Cart
    {
        public virtual Guid Id { get; protected set; }
        public virtual IDictionary<string, int> Items { get; set; }

        public Cart()
        {
            Items = new Dictionary<string, int>();
        }

        public virtual int TotalItems()
        {
            return Items.Count;
        }

        public virtual void AddItemSomeTimes(string aBook, int aNumber)
        {
            if (HasABook(aBook))
            {
                Items[aBook] += aNumber;
                return;
            }
            Items.Add(aBook,aNumber);
        }

        public virtual bool HasABook(string aBook)
        {
            return Items.ContainsKey(aBook);
        }

        public virtual bool IsEmpty()
        {
            return Items.IsEmpty();
        }

        public virtual int QuantityOf(String aBook)
        {
            return Items[aBook];
        }

        public List<SaleDetail> CreateSaleDetailWith(IDictionary aCatalog)
        {
            var details = new List<SaleDetail>();
            
            var books = Items.Keys;
            books.ForEach(aBook => details.Add(new SaleDetail(aBook, QuantityOf(aBook), (int) aCatalog[aBook])));//TODO: revisar el casteo, pero fijarse el tema de la DB
            return details;

        }
    }
}
