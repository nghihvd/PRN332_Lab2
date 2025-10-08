namespace PRN232.Lab2.CoffeeStore.Services.Models.Responses
{
    public class PaymentResponseModel
    {
        public string PaymentId { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
        public string Status { get; set; } = null!; // Active / Inactive
    }
}
