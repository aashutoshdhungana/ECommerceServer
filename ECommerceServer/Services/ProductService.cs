using System;
using System.Threading.Tasks;
using ECommerceServer.Models;
using ECommerceServer.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceServer.Services
{
    public class ProductService : IProductService
    {
        private readonly ECommerceContext _context;
        public ProductService(ECommerceContext context)
        {
            _context = context;
        }

        public async Task CreateProductAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            
            await _context.Products.AddAsync(product);
        }

        public void DeleteProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _context.Products.Remove(product);
        }

        public void UpdateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _context.Products.Update(product);
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        }
        public async Task<bool> SaveChangeAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public IEnumerable<Product> GetProductsByUserId(Guid userid)
        {
            return _context.Products.Where(x => x.UserId == userid);
        }
    }
}
