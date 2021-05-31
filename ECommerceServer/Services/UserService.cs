using System;
using System.Threading.Tasks;
using ECommerceServer.Models;
using ECommerceServer.Database;
using Microsoft.EntityFrameworkCore;

namespace ECommerceServer.Services
{
    public class UserService : IUserService
    {
        private readonly ECommerceContext _context;

        public UserService(ECommerceContext context)
        {
            _context = context;
        }

        public static AuthenticateResponse GetReponseUser(User user, string token)
        {
            return new AuthenticateResponse()
            {
                Id = user.UserId.ToString(),
                UserName = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DOB = user.DOB,
                Wallet = user.Wallet,
                JWTToken = token
            };
        }

        public static bool ValidateUser(User user,string password)
        {
            if (user == null)
                return false;
            return user.Password == AccountUtil.PasswordHasher(password);
        }

        public async Task CreateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _context.Users.AddAsync(user);
        }

        public void DeleteUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Remove(user);
        }

        public void UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Update(user);
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.UserId == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Email == email);
        }
    }
}
