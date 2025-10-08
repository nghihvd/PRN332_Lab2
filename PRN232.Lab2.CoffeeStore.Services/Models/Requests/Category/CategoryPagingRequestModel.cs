namespace PRN232.Lab2.CoffeeStore.Services.Models.Requests.Category
{
    public class CategoryPagingRequestModel
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public string SortBy { get; set; } = "Name";
        public bool IsDescending { get; set; } = false;
    }
}
