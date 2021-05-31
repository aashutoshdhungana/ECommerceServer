using ECommerceServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceServer.Database
{
    public class ECommerceContext : DbContext
    {
        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
