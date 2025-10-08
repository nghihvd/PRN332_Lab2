using System.ComponentModel.DataAnnotations;

namespace PRN232.Lab2.CoffeeStore.Services.Models.Requests.Payment
{
    public class CreatePaymentRequestModel
    {
        [Required]
        public string OrderId { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = "Cash"; // e.g., Cash, Credit Card
    }
}
