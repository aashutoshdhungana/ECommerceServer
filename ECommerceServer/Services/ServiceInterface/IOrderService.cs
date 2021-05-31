using System;
using System.Threading.Tasks;
using ECommerceServer.Models;

namespace ECommerceServer.Services
{
    public interface IOrderService
    {
        Task<bool> saveChangesAsync();
        Task CreateOrderAsync(Order order);
        void UpdateOrder(Order order);
        Task<Order> GetOrderAsync(Guid id);
    }
}
