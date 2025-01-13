using ECommerceApi.Models;
using System.Threading.Tasks;

namespace ECommerceApi.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
    }
}