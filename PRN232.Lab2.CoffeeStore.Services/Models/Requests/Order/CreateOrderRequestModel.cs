using System.ComponentModel.DataAnnotations;

namespace PRN232.Lab2.CoffeeStore.Services.Models.Requests.Order
{
    public class CreateOrderRequestModel
    {
        public string? UserId { get; set; }
        public string? PaymentId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Đơn hàng phải có ít nhất một sản phẩm.")]
        public List<CreateOrderDetailRequestModel> OrderDetails { get; set; } = new List<CreateOrderDetailRequestModel>();
    }

    public class CreateOrderDetailRequestModel
    {
        [Required]
        public string ProductId { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0.")]
        public int Quantity { get; set; }
    }
}
