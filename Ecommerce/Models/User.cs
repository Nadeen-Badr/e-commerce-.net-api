using Microsoft.AspNetCore.Identity;

namespace ECommerceApi.Models
{
    public class User : IdentityUser
    {
        public string? Role { get; set; } // Buyer or Seller
    }
}