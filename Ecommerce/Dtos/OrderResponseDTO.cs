 public class OrderResponseDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemResponseDTO> OrderItems { get; set; }
    }