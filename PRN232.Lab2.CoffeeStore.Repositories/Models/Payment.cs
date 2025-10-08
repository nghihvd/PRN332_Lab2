namespace PRN232.Lab2.CoffeeStore.Repositories.Models
{
    public class Payment
    {
        public string PaymentId { get; set; } = Guid.NewGuid().ToString();
        public string PaymentMethod { get; set; } = null!;
        public string Status { get; set; } = "Active"; // e.g., Active/Inactive for payment method
    }
}
