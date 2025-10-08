namespace PRN232.Lab2.CoffeeStore.Services.Models.Responses
{
    public class CategoryResponseModel
    {
        public string CategoryId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ProductCount { get; set; }
    }
}
