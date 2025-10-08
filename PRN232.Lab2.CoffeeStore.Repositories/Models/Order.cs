namespace PRN232.Lab2.CoffeeStore.Repositories.Models
{
    public class Order
    {
        public string OrderId { get; set; } = Guid.NewGuid().ToString();
        public string? UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public string? PaymentId { get; set; }

        // Navigation Properties
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public Payment? Payment { get; set; }
    }
}
