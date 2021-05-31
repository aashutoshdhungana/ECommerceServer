using System;
using System.Threading.Tasks;
using ECommerceServer.Database;
using ECommerceServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceServer.Services
{
    public class OrderService : IOrderService
    {
        private readonly ECommerceContext _context;
        public OrderService(ECommerceContext context)
        {
            _context = context;
        }

        public void UpdateOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException();
            _context.Orders.Update(order);
        }

        public async Task CreateOrderAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException();
            await _context.Orders.AddAsync(order);
        }

        public async Task<bool> saveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<Order> GetOrderAsync(Guid id)
        {
            return (await _context.Orders.FirstOrDefaultAsync(order => order.OrderId == id));
        }
    }
}
