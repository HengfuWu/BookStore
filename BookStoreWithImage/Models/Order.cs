using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreWithImage.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public double Total { get; set; }
    }
}