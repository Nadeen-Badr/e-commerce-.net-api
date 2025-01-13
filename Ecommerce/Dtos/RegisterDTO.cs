using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [RegularExpression("^(Buyer|Seller)$", ErrorMessage = "Role must be either 'Buyer' or 'Seller'.")]
        public string Role { get; set; } // Buyer or Seller
    }
}