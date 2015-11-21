using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreWithImage.Models
{
    public class BookProviders
    {
        static BookProviders()
        {
            Instance = new BookProviders();
            Instance.AllBooks = new List<Books>();
            for (int i = 1; i <= 50; i++)
            {
                Books book = new Books() { Id = i, Name = String.Format("Book #{0}", i), Price = i };
                Instance.AllBooks.Add(book);
            }
            Counter = 0;
        }
        public List<Books> AllBooks { get; private set; }
        public static BookProviders Instance { get; private set; }

        public static int Counter;
    }
}