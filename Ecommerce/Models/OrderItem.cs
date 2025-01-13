namespace ECommerceApi.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; } // Foreign key to Product
        public int Quantity { get; set; }
        public decimal Price { get; set; }
         public Product Product { get; set; }
    }
}