using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceServer.Models;

namespace ECommerceServer.Services
{
    public interface ITransactionService
    {
        Task CreateTransactionAsync(Transaction transaction);
        Task<bool> SaveChangesAsync();
        IEnumerable<Transaction> GetTransactionsByUserId(Guid userId);
        Task<Transaction> GetTransactionByIdAsync(Guid id);
        
    }
}
