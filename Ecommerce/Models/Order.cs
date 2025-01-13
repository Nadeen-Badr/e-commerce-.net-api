namespace ECommerceApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string BuyerId { get; set; } // Foreign key to User
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public User Buyer { get; set; }
    }
}