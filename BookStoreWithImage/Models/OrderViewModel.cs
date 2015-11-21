namespace BookStoreWithImage.Models
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public Books Book { get; set; }
        public User User { get; set; }
    }
}