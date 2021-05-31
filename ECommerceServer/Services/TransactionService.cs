using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceServer.Models;
using ECommerceServer.Database;
using Microsoft.EntityFrameworkCore;

namespace ECommerceServer.Services
{
    public class TransactionService : ITransactionService
    {
        ECommerceContext _context;
        public TransactionService(ECommerceContext context)
        {
            _context = context;
        }

        public async Task CreateTransactionAsync(Transaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException();
            }
            await _context.Transactions.AddAsync(transaction);
        }

        public async Task<Transaction> GetTransactionByIdAsync(Guid id)
        {
            return await _context.Transactions.FirstOrDefaultAsync(x => x.TrasanctionId == id);
        }

        public IEnumerable<Transaction> GetTransactionsByUserId(Guid userId)
        {
            return  _context.Transactions.Where(x => x.PayeeId == userId || x.PayeeId == userId ).AsEnumerable();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
