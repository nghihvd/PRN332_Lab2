namespace PRN232.Lab2.CoffeeStore.Services.Models.Responses
{
    public class OrderResponseModel
    {
        public string OrderId { get; set; } = null!;
        public string? UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderDetailResponseModel> OrderDetails { get; set; } = new List<OrderDetailResponseModel>();
        public PaymentResponseModel? Payment { get; set; }
    }

    public class OrderDetailResponseModel
    {
        public string OrderDetailId { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}
