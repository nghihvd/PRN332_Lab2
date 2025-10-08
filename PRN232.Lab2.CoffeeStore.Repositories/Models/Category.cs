namespace PRN232.Lab2.CoffeeStore.Repositories.Models
{
    public class Category
    {
        public string CategoryId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation Property
        public ICollection<Product>? Products { get; set; }
    }
}
