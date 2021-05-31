using System;
using ECommerceServer.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ECommerceServer.Services
{
    public interface IProductService
    {
        Task<bool> SaveChangeAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        IEnumerable<Product> GetProductsByUserId(Guid userid);
        Task CreateProductAsync(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
