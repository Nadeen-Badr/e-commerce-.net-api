using ECommerceApi.Data;
using ECommerceApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerceApi.Repositories
{
    public class OrderRepository:IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Order>> GetOrderHistoryAsync(string buyerId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.BuyerId == buyerId)
                .ToListAsync();
        }
    }
}