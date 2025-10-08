namespace PRN232.Lab2.CoffeeStore.Repositories.Models
{
    public class OrderDetail
    {
        public string OrderDetailId { get; set; } = Guid.NewGuid().ToString();
        public string? OrderId { get; set; }
        public string? ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Navigation
        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
