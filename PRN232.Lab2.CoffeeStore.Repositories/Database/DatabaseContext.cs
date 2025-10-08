using Microsoft.EntityFrameworkCore;
using PRN232.Lab2.CoffeeStore.Repositories.Models;

namespace PRN232.Lab2.CoffeeStore.Repositories.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Decimal precision mapping to avoid truncation
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.UnitPrice)
                .HasColumnType("decimal(18,2)");

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = "11111111-1111-1111-1111-111111111111", Name = "Coffee Beans", Description = "Various coffee beans", CreatedDate = new DateTime(2025, 1, 1) },
                new Category { CategoryId = "22222222-2222-2222-2222-222222222222", Name = "Coffee Machines", Description = "Brewing machines", CreatedDate = new DateTime(2025, 2, 1) },
                new Category { CategoryId = "33333333-3333-3333-3333-333333333333", Name = "Accessories", Description = "Coffee accessories", CreatedDate = new DateTime(2025, 3, 1) },
                new Category { CategoryId = "44444444-4444-4444-4444-444444444444", Name = "Tea", Description = "Assorted tea leaves", CreatedDate = new DateTime(2025, 4, 1) },
                new Category { CategoryId = "55555555-5555-5555-5555-555555555555", Name = "Snacks", Description = "Coffee snacks", CreatedDate = new DateTime(2025, 5, 1) }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = "aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1", Name = "Arabica Beans", Description = "Smooth coffee beans", Price = 15.50m, CategoryId = "11111111-1111-1111-1111-111111111111", IsActive = true },
                new Product { ProductId = "aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2", Name = "Robusta Beans", Description = "Strong coffee beans", Price = 12.00m, CategoryId = "11111111-1111-1111-1111-111111111111", IsActive = true },
                new Product { ProductId = "aaaaaaa3-aaaa-aaaa-aaaa-aaaaaaaaaaa3", Name = "Espresso Machine", Description = "Automatic espresso maker", Price = 250.00m, CategoryId = "22222222-2222-2222-2222-222222222222", IsActive = true },
                new Product { ProductId = "aaaaaaa4-aaaa-aaaa-aaaa-aaaaaaaaaaa4", Name = "Coffee Grinder", Description = "Electric grinder", Price = 45.00m, CategoryId = "33333333-3333-3333-3333-333333333333", IsActive = true },
                new Product { ProductId = "aaaaaaa5-aaaa-aaaa-aaaa-aaaaaaaaaaa5", Name = "Green Tea", Description = "Refreshing green tea", Price = 8.00m, CategoryId = "44444444-4444-4444-4444-444444444444", IsActive = true }
            );

            // Seed Orders
            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = "bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1", UserId = "user01", OrderDate = new DateTime(2025, 6, 1), Status = "Completed", PaymentId = "pay-cc" },
                new Order { OrderId = "bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2", UserId = "user02", OrderDate = new DateTime(2025, 6, 2), Status = "Pending", PaymentId = "pay-pp" },
                new Order { OrderId = "bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3", UserId = "user03", OrderDate = new DateTime(2025, 6, 3), Status = "Shipped", PaymentId = "pay-cc" },
                new Order { OrderId = "bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4", UserId = "user01", OrderDate = new DateTime(2025, 6, 4), Status = "Cancelled", PaymentId = "pay-na" },
                new Order { OrderId = "bbbbbbb5-bbbb-bbbb-bbbb-bbbbbbbbbbb5", UserId = "user04", OrderDate = new DateTime(2025, 6, 5), Status = "Completed", PaymentId = "pay-dc" }
            );

            // Seed OrderDetails
            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail { OrderDetailId = "ccccccc1-cccc-cccc-cccc-ccccccccccc1", OrderId = "bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1", ProductId = "aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1", Quantity = 2, UnitPrice = 15.50m },
                new OrderDetail { OrderDetailId = "ccccccc2-cccc-cccc-cccc-ccccccccccc2", OrderId = "bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1", ProductId = "aaaaaaa4-aaaa-aaaa-aaaa-aaaaaaaaaaa4", Quantity = 1, UnitPrice = 45.00m },
                new OrderDetail { OrderDetailId = "ccccccc3-cccc-cccc-cccc-ccccccccccc3", OrderId = "bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2", ProductId = "aaaaaaa3-aaaa-aaaa-aaaa-aaaaaaaaaaa3", Quantity = 1, UnitPrice = 250.00m },
                new OrderDetail { OrderDetailId = "ccccccc4-cccc-cccc-cccc-ccccccccccc4", OrderId = "bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3", ProductId = "aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2", Quantity = 3, UnitPrice = 12.00m },
                new OrderDetail { OrderDetailId = "ccccccc5-cccc-cccc-cccc-ccccccccccc5", OrderId = "bbbbbbb5-bbbb-bbbb-bbbb-bbbbbbbbbbb5", ProductId = "aaaaaaa5-aaaa-aaaa-aaaa-aaaaaaaaaaa5", Quantity = 5, UnitPrice = 8.00m }
            );

            // Seed Payment methods (catalog)
            modelBuilder.Entity<Payment>().HasData(
                new Payment { PaymentId = "pay-cc", PaymentMethod = "Credit Card", Status = "Active" },
                new Payment { PaymentId = "pay-pp", PaymentMethod = "PayPal", Status = "Active" },
                new Payment { PaymentId = "pay-dc", PaymentMethod = "Debit Card", Status = "Active" },
                new Payment { PaymentId = "pay-cash", PaymentMethod = "Cash", Status = "Active" },
                new Payment { PaymentId = "pay-na", PaymentMethod = "N/A", Status = "Inactive" }
            );
        }

    }
}
