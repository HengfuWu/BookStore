using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreWithImage.Models
{
    public class BuyViewModel
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Amount { get; set; }
        public int Limit { get; set; }
    }
}