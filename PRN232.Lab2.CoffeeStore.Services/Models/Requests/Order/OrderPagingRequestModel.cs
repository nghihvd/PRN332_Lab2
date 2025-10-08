namespace PRN232.Lab2.CoffeeStore.Services.Models.Requests.Order
{
    public class OrderPagingRequestModel
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string? UserId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string SortBy { get; set; } = "OrderDate";
        public bool IsDescending { get; set; } = true;
    }
}
