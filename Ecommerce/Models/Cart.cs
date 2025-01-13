namespace ECommerceApi.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string BuyerId { get; set; } // Foreign key to User (Buyer)
        public int ProductId { get; set; } // Foreign key to Product
        public int Quantity { get; set; }
        public Product Product { get; set; } // Navigation property to Product
    }
}