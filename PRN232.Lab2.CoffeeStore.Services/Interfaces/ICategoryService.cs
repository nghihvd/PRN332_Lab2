using PRN232.Lab2.CoffeeStore.Services.Commons;
using PRN232.Lab2.CoffeeStore.Services.Models.Requests.Category;
using PRN232.Lab2.CoffeeStore.Services.Models.Responses;

namespace PRN232.Lab2.CoffeeStore.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<PagedResult<CategoryResponseModel>> GetPagingAsync(CategoryPagingRequestModel request);
        Task<CategoryResponseModel?> GetByIdAsync(string id);
        Task<CategoryResponseModel> CreateAsync(CreateCategoryRequestModel request);
        Task<CategoryResponseModel> UpdateAsync(string id, UpdateCategoryRequestModel request);
        Task<bool> DeleteAsync(string id);
    }
}
