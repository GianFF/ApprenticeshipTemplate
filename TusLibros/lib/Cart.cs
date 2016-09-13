using System;
using System.Collections.Generic;
using System.Linq;

namespace TusLibros.lib
{
    public class Cart : System.Object
    {
        //internal static string ErrorMessageForInvalidBook = "You are trying to add an invalid book";
        protected List<string> Items = new List<string>();

        public List<string> GetItems()
        {
            return Items;
        }
        internal int TotalItems()
        {
            return Items.Count;
        }

        internal void AddItem(string aBook)
        {
            //if (aBook == "Book from other editorial")
              //  throw new ArgumentException(ErrorMessageForInvalidBook);
            Items.Add(aBook);
        }

        internal void AddItemSomeTimes(string aBook, int aNumber)
        {
            for (int i = 0; i < aNumber; i++)
            {
                Items.Add(aBook);
            }
        }

        internal bool HasABook(string aBook)
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

            return Items.Equals(aCart.Items);
        }

        public bool Equals(Cart otherCart)
        {
            
            if ((object)otherCart == null)
            {
                return false;
            }

            // Return true if the fields match:
            return Items.Equals(otherCart.Items);
        }

        public override int GetHashCode()
        {
            return Items.GetHashCode();
        }
    }
}
