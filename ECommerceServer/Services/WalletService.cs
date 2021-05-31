using System;
using System.Threading.Tasks;
using ECommerceServer.Database;
using ECommerceServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceServer.Services
{
    public class WalletService : IWalletService
    {
        ECommerceContext _context;
        public WalletService (ECommerceContext context)
        {
            _context = context;
        }
        public async Task<bool> LoadWalletAsync(Guid id, double amt)
        {
            try
            {
                var validUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
                if (validUser == null)
                {
                    return false;
                }

                validUser.Wallet += amt;
                _context.Users.Update(validUser);
                return (await _context.SaveChangesAsync() >= 0);
            }

            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UnloadWalletAsync(Guid id, double amt)
        {
            try
            {
                var validUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
                if (validUser == null)
                {
                    return false;
                }
                if (validUser.Wallet < amt)
                {
                    return false;
                }
                validUser.Wallet -= amt;
                _context.Users.Update(validUser);
                return (await _context.SaveChangesAsync() >= 0);
            }

            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> PerformTransactionAsync(Transaction transaction)
        {
            if (await UnloadWalletAsync(transaction.PayerId, transaction.Amount))
            {
                if (await LoadWalletAsync(transaction.PayeeId, transaction.Amount))
                {
                    return true;
                }
                await LoadWalletAsync(transaction.PayerId, transaction.Amount);
            }
            return false;
        }
    }
}
