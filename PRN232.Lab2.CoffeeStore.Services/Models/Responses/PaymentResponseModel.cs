namespace PRN232.Lab2.CoffeeStore.Services.Models.Responses
{
    public class PaymentResponseModel
    {
        public string PaymentId { get; set; } = null!;
        public string OrderId { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string Status { get; set; } = null!; // e.g., Completed, Pending, Failed
    }
}
