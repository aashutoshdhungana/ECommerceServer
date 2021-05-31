using System;
using System.Threading.Tasks;
using ECommerceServer.Models;

namespace ECommerceServer.Services
{
    public interface IUserService
    {
        Task<bool> SaveChangesAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByEmailAsync(string email);
        Task CreateUserAsync(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
