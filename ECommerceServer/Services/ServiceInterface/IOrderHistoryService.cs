using System;
using System.Threading.Tasks;
using ECommerceServer.Models;

namespace ECommerceServer.Services
{
    public interface IOrderHistoryService
    {
        Task CreateOrderHistoryAsync(OrderHistory orderHistory);
        void UpdateOrderHistory(OrderHistory orderHistory);
        Task<OrderHistory> GetOrderHistoryByIdAsync(Guid id);
        Task<OrderHistory> GetOrderHistoryByUserIdAsync(Guid userId);
        Task<bool> SaveChangesAsync();
    }
}
