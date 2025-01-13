namespace ECommerceApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? SellerId { get; set; } // Foreign key to User
         public User? Seller { get; set; }
    }
}