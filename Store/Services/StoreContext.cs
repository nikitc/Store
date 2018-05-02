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
        public DbSet<Product2Order> Product2Orders { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Brand> Brands { get; set; }

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

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId);

            modelBuilder.Entity<Product2Order>()
                .HasKey(t => new { t.ProductId, t.OrderId });
        }
    }
}