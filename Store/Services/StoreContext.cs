using Microsoft.EntityFrameworkCore;
using Store.Database.Entities;

namespace Store.Services
{
    public class StoreContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }

        public StoreContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Order>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Order>()
                .Property(f => f.Number)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<OrderState>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Product2Order>()
                .HasKey(t => new { t.ProductId, t.OrderId });

            modelBuilder.Entity<Product2Order>()
                .HasOne(sc => sc.Product)
                .WithMany(s => s.Product2Orders)
                .HasForeignKey(sc => sc.ProductId);

            modelBuilder.Entity<Product2Order>()
                .HasOne(sc => sc.Order)
                .WithMany(c => c.Product2Orders)
                .HasForeignKey(sc => sc.OrderId);
        }
    }
}