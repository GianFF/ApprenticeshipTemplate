﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            return Items.Values.Sum();
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

        public virtual List<SaleDetail> CreateSaleDetailWith(IDictionary<string, int> aCatalog)
        {
            var details = new List<SaleDetail>();
            
            var books = Items.Keys;
            books.ForEach(aBook => details.Add(new SaleDetail(aBook, QuantityOf(aBook), aCatalog[aBook])));
            return details;
        }

        public IEnumerable<T> CollectItems<T>(Func<string, T> aBlock)
        {
            return Items.Keys.Select(aBlock);
        }


        public virtual bool VerifyIfContainsInvalidBooks(IDictionary<string, int> aCatalog)
        {
            return Items.Keys.Any(aBook => !aCatalog.ContainsKey(aBook));
        }

        public virtual IList<int> MapItemsToPrices(IDictionary<string,int> aCatalog)
        {
            IList<int> lista = new List<int>();
            Items.Keys.ForEach(aBook => lista.Add(aCatalog[aBook] * Items[aBook]));
            return lista;
        }
    }
}
