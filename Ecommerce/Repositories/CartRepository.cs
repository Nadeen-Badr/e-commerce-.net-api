using ECommerceApi.Data;
using ECommerceApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApi.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cart>> GetCartItemsAsync(string buyerId)
        {
            return await _context.Carts
                .Include(c => c.Product) // Include the Product details
                .Where(c => c.BuyerId == buyerId)
                .ToListAsync();
        }

        public async Task AddToCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(int cartItemId)
        {
            var cartItem = await _context.Carts.FindAsync(cartItemId);
            if (cartItem != null)
            {
                _context.Carts.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Cart> GetCartItemByIdAsync(int cartItemId)
        {
            return await _context.Carts
                .Include(c => c.Product) // Include the Product details
                .FirstOrDefaultAsync(c => c.Id == cartItemId);
        }
    }
}