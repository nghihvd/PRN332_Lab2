using PRN232.Lab2.CoffeeStore.Services.Commons;
using PRN232.Lab2.CoffeeStore.Services.Models.Requests.Payment;
using PRN232.Lab2.CoffeeStore.Services.Models.Responses;

namespace PRN232.Lab2.CoffeeStore.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponseModel> CreateAsync(CreatePaymentRequestModel request);
        Task<PaymentResponseModel?> GetByIdAsync(string id);
        Task<PagedResult<PaymentResponseModel>> GetPagingAsync(PaymentPagingRequestModel request);
        Task<PaymentResponseModel> UpdateAsync(string id, UpdatePaymentRequestModel request);
        Task<bool> DeleteAsync(string id);
    }
}
