using System;
using System.Threading.Tasks;
using ECommerceServer.Models;
using ECommerceServer.Database;
using Microsoft.EntityFrameworkCore;
namespace ECommerceServer.Services
{
    public class OrderHistoryService : IOrderHistoryService
    {
        readonly ECommerceContext _context;

        public OrderHistoryService(ECommerceContext context)
        {
            _context = context;
        }

        public async Task CreateOrderHistoryAsync(OrderHistory orderHistory)
        {
            if (orderHistory == null)
                throw new ArgumentNullException();
            await _context.OrderHistories.AddAsync(orderHistory);
        }

        public async Task<OrderHistory> GetOrderHistoryByIdAsync(Guid id)
        {
            return (await _context.OrderHistories.FirstOrDefaultAsync(oh => oh.OrderHistoryId == id));
        }

        public async Task<OrderHistory> GetOrderHistoryByUserIdAsync(Guid userId)
        {
            return (await _context.OrderHistories.Include(oh => oh.Orders).FirstOrDefaultAsync(oh => oh.UserId == userId));
        }



        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void UpdateOrderHistory(OrderHistory orderHistory)
        {
            if (orderHistory == null)
                throw new ArgumentNullException();
            _context.OrderHistories.Update(orderHistory);
        }
    }
}
