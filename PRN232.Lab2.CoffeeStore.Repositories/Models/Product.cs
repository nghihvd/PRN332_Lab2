namespace PRN232.Lab2.CoffeeStore.Repositories.Models
{
    public class Product
    {
        public string ProductId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? CategoryId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        // Navigation
        public Category? Category { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
