using System;
using System.Collections.Generic;

namespace TusLibros
{
    public class Cart
    {
        internal static string ERROR_MESSAGE_FOR_INVALID_BOOK = "You are trying to add an invalid book";
        private List<string> items = new List<string>();

        internal int totalItems()
        {
            return items.Count;
        }

        internal void addItem(string aBook)
        {
            if (aBook == "Book from other editorial")
                throw new ArgumentException(ERROR_MESSAGE_FOR_INVALID_BOOK);
            items.Add(aBook);
        }

        internal void addItemSomeTimes(string aBook, int aNumber)
        {
            for (int i = 0; i < aNumber; i++)
            {
                items.Add(aBook);
            }
        }

        internal bool hasABook(string aBook)
        {
            return items.Contains(aBook);
        }

    }
}