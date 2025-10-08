namespace PRN232.Lab2.CoffeeStore.Repositories.Models
{
    public class Payment
    {
        public string PaymentId { get; set; } = Guid.NewGuid().ToString();
        public string? OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string Status { get; set; } = "Pending"; // e.g., Completed, Pending, Failed

        // Navigation
        public Order? Order { get; set; }
    }
}
