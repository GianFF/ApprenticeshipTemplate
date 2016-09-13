using System;
using System.Collections.Generic;
using System.Linq;

namespace TusLibros.lib
{

    public class Cart : Entity

    {
        public List<string> Items;

        public Cart()
        {
            Items = new List<string>();
        }

        public int TotalItems()
        {
            return Items.Count;
        }

        public void AddItem(string aBook)
        {
            Items.Add(aBook);
        }

        public void AddItemSomeTimes(string aBook, int aNumber)
        {
            for (int i = 0; i < aNumber; i++)
            {
                Items.Add(aBook);
            }
        }

        public bool HasABook(string aBook)
        {
            return Items.Contains(aBook);
        }

        public bool IsEmpty()
        {
            return !Items.Any();
        }

        public int QuantityOf(String aBook)
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
