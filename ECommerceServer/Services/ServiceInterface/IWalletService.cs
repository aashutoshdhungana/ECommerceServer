using System;
using ECommerceServer.Models;
using System.Threading.Tasks;

namespace ECommerceServer.Services
{
    public interface IWalletService
    {
        Task<bool> LoadWalletAsync(Guid id, Double amt);
        Task<bool> UnloadWalletAsync(Guid id, Double amt);
        Task<bool> PerformTransactionAsync(Transaction transaction);
    }
}
