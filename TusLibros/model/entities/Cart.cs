using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static TusLibros.model.TusLibrosApp;

namespace TusLibros.model.entities
{

    public class Cart
    {
        public virtual Guid Id { get; protected set; }
        public virtual IList<string> Items { get; set; }

        public Cart()
        {
            Items = new List<string>();
        }

        public virtual int TotalItems()
        {
            return Items.Count;
        }

        public virtual void AddItemSomeTimes(string aBook, int aNumber)
        {
            RepeatAction(aNumber, () => { Items.Add(aBook); });
        }

        public virtual bool HasABook(string aBook)
        {
            return Items.Contains(aBook);
        }

        public virtual bool IsEmpty()
        {
            return !Items.Any();
        }

        public virtual int QuantityOf(String aBook)
        {
            return Items.Count(book => book == aBook);
        }

        public virtual IDictionary ListBooksWithOccurrences()
        {
            var listBooksWithOccurrences = new Dictionary<string,int>();
            var differentBooks = Items.Distinct().ToList();
            differentBooks.ForEach(book => listBooksWithOccurrences.Add(book, QuantityOf(book)));
            return listBooksWithOccurrences;
        }
    }
}
