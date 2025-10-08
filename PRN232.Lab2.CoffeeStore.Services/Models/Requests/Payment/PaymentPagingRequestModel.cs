namespace PRN232.Lab2.CoffeeStore.Services.Models.Requests.Payment
{
    public class PaymentPagingRequestModel
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string? Status { get; set; }
        public string SortBy { get; set; } = "PaymentMethod"; // PaymentMethod | Status
        public bool IsDescending { get; set; } = true;
    }
}


