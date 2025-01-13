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
    }
}