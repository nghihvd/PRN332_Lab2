using PRN232.Lab2.CoffeeStore.Services.Commons;
using PRN232.Lab2.CoffeeStore.Services.Models.Requests.Order;
using PRN232.Lab2.CoffeeStore.Services.Models.Responses;

namespace PRN232.Lab2.CoffeeStore.Services.Interfaces
{
    public interface IOrderService
    {
        Task<PagedResult<OrderResponseModel>> GetPagingAsync(OrderPagingRequestModel request);
        Task<OrderResponseModel?> GetByIdAsync(string id);
        Task<OrderResponseModel> CreateAsync(CreateOrderRequestModel request, string userId);
        Task<OrderResponseModel> UpdateAsync(string id, CreateOrderRequestModel request, string userId);
        Task<bool> DeleteAsync(string id);
    }
}
