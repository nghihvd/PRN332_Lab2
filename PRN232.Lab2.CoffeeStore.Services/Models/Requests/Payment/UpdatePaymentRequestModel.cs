using System.ComponentModel.DataAnnotations;

namespace PRN232.Lab2.CoffeeStore.Services.Models.Requests.Payment
{
    public class UpdatePaymentRequestModel
    {
        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = null!; // Completed | Pending | Failed
    }
}


