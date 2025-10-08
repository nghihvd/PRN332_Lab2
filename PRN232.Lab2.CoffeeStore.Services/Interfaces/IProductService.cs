using PRN232.Lab2.CoffeeStore.Services.Commons;
using PRN232.Lab2.CoffeeStore.Services.Models.Requests.Product;
using PRN232.Lab2.CoffeeStore.Services.Models.Responses;

namespace PRN232.Lab2.CoffeeStore.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponseModel?> GetByIdAsync(string productId);
        Task<PagedResult<ProductResponseModel>> GetPagingAsync(ProductPagingRequestModel request);
        Task<ProductResponseModel> CreateAsync(CreateProductRequestModel productCreateDto);
        Task<ProductResponseModel> UpdateAsync(string id,UpdateProductRequestModel productUpdateDto);
        Task<bool> DeleteAsync(string productId);
    }
}
