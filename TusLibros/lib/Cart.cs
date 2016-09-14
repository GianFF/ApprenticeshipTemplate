using System;
using System.Collections.Generic;
using System.Linq;

namespace TusLibros.lib
{

    public class Cart

    {
        public virtual int Id { get; protected set; }
        public virtual List<string> Items { get; set; }

        public Cart()
        {
            Items = new List<string>();
        }

        public virtual int TotalItems()
        {
            return Items.Count;
        }

        public virtual void AddItem(string aBook)
        {
            Items.Add(aBook);
        }

        public virtual void AddItemSomeTimes(string aBook, int aNumber)
        {
            for (int i = 0; i < aNumber; i++)
            {
                Items.Add(aBook);
            }
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
            return Items.Count(book => book == aBook );
        }
        
        public override bool Equals(System.Object otherCart)
        {
            if (otherCart == null)
            {
                return false;
            }

            Cart aCart = otherCart as Cart;
            if ((System.Object)aCart == null)
            {
                return false;
            }
            
            return Items.Count == aCart.Items.Count;
        }
    }
}
