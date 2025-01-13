using ECommerceApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApi.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetCartItemsAsync(string buyerId);
        Task AddToCartAsync(Cart cart);
        Task RemoveFromCartAsync(int cartItemId);
        Task<Cart> GetCartItemByIdAsync(int cartItemId);
        Task ClearCartAsync(string buyerId);
    }
}