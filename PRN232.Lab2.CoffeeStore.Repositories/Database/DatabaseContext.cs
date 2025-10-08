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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = "11111111-1111-1111-1111-111111111111", Name = "Coffee Beans", Description = "Various coffee beans", CreatedDate = DateTime.Now.AddMonths(-5) },
                new Category { CategoryId = "22222222-2222-2222-2222-222222222222", Name = "Coffee Machines", Description = "Brewing machines", CreatedDate = DateTime.Now.AddMonths(-4) },
                new Category { CategoryId = "33333333-3333-3333-3333-333333333333", Name = "Accessories", Description = "Coffee accessories", CreatedDate = DateTime.Now.AddMonths(-3) },
                new Category { CategoryId = "44444444-4444-4444-4444-444444444444", Name = "Tea", Description = "Assorted tea leaves", CreatedDate = DateTime.Now.AddMonths(-2) },
                new Category { CategoryId = "55555555-5555-5555-5555-555555555555", Name = "Snacks", Description = "Coffee snacks", CreatedDate = DateTime.Now.AddMonths(-1) }
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
                new Order { OrderId = "bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1", UserId = "user01", OrderDate = DateTime.Now.AddDays(-10), Status = "Completed" },
                new Order { OrderId = "bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2", UserId = "user02", OrderDate = DateTime.Now.AddDays(-9), Status = "Pending" },
                new Order { OrderId = "bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3", UserId = "user03", OrderDate = DateTime.Now.AddDays(-8), Status = "Shipped" },
                new Order { OrderId = "bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4", UserId = "user01", OrderDate = DateTime.Now.AddDays(-7), Status = "Cancelled" },
                new Order { OrderId = "bbbbbbb5-bbbb-bbbb-bbbb-bbbbbbbbbbb5", UserId = "user04", OrderDate = DateTime.Now.AddDays(-6), Status = "Completed" }
            );

            // Seed OrderDetails
            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail { OrderDetailId = "ccccccc1-cccc-cccc-cccc-ccccccccccc1", OrderId = "bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1", ProductId = "aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1", Quantity = 2, UnitPrice = 15.50m },
                new OrderDetail { OrderDetailId = "ccccccc2-cccc-cccc-cccc-ccccccccccc2", OrderId = "bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1", ProductId = "aaaaaaa4-aaaa-aaaa-aaaa-aaaaaaaaaaa4", Quantity = 1, UnitPrice = 45.00m },
                new OrderDetail { OrderDetailId = "ccccccc3-cccc-cccc-cccc-ccccccccccc3", OrderId = "bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2", ProductId = "aaaaaaa3-aaaa-aaaa-aaaa-aaaaaaaaaaa3", Quantity = 1, UnitPrice = 250.00m },
                new OrderDetail { OrderDetailId = "ccccccc4-cccc-cccc-cccc-ccccccccccc4", OrderId = "bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3", ProductId = "aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2", Quantity = 3, UnitPrice = 12.00m },
                new OrderDetail { OrderDetailId = "ccccccc5-cccc-cccc-cccc-ccccccccccc5", OrderId = "bbbbbbb5-bbbb-bbbb-bbbb-bbbbbbbbbbb5", ProductId = "aaaaaaa5-aaaa-aaaa-aaaa-aaaaaaaaaaa5", Quantity = 5, UnitPrice = 8.00m }
            );

            // Seed Payments
            modelBuilder.Entity<Payment>().HasData(
                new Payment { PaymentId = "ddddddd1-dddd-dddd-dddd-dddddddddddd1", OrderId = "bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1", Amount = 76.00m, PaymentDate = DateTime.Now.AddDays(-9), PaymentMethod = "Credit Card" },
                new Payment { PaymentId = "ddddddd2-dddd-dddd-dddd-dddddddddddd2", OrderId = "bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2", Amount = 250.00m, PaymentDate = DateTime.Now.AddDays(-8), PaymentMethod = "PayPal" },
                new Payment { PaymentId = "ddddddd3-dddd-dddd-dddd-dddddddddddd3", OrderId = "bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbb3", Amount = 36.00m, PaymentDate = DateTime.Now.AddDays(-7), PaymentMethod = "Credit Card" },
                new Payment { PaymentId = "ddddddd4-dddd-dddd-dddd-dddddddddddd4", OrderId = "bbbbbbb5-bbbb-bbbb-bbbb-bbbbbbbbbbb5", Amount = 40.00m, PaymentDate = DateTime.Now.AddDays(-5), PaymentMethod = "Debit Card" },
                new Payment { PaymentId = "ddddddd5-dddd-dddd-dddd-dddddddddddd5", OrderId = "bbbbbbb4-bbbb-bbbb-bbbb-bbbbbbbbbbb4", Amount = 0.00m, PaymentDate = DateTime.Now.AddDays(-6), PaymentMethod = "N/A" }// Cancelled order
            );
        }

    }
}
