using PRN232.Lab2.CoffeeStore.Services.Models.Requests.Payment;
using PRN232.Lab2.CoffeeStore.Services.Models.Responses;

namespace PRN232.Lab2.CoffeeStore.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponseModel> CreatePaymentAsync(CreatePaymentRequestModel request);
        Task<PaymentResponseModel?> GetByOrderIdAsync(string orderId);
    }
}
