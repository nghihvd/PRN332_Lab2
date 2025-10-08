using Microsoft.EntityFrameworkCore;
using PRN232.Lab2.CoffeeStore.Repositories.Interfaces.UOW;
using PRN232.Lab2.CoffeeStore.Repositories.Models;
using PRN232.Lab2.CoffeeStore.Services.Interfaces;
using PRN232.Lab2.CoffeeStore.Services.Models.Requests.Payment;
using PRN232.Lab2.CoffeeStore.Services.Models.Responses;

namespace PRN232.Lab2.CoffeeStore.Services.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaymentResponseModel> CreatePaymentAsync(CreatePaymentRequestModel request)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var orderRepository = _unitOfWork.GetRepository<Order>();
                var order = await orderRepository.GetByIdAsync(request.OrderId);

                if (order == null)
                {
                    throw new KeyNotFoundException("Không tìm thấy đơn hàng.");
                }

                if (order.Payment != null)
                {
                    throw new InvalidOperationException("Đơn hàng đã được thanh toán.");
                }

                var paymentRepository = _unitOfWork.GetRepository<Payment>();
                var payment = new Payment
                {
                    PaymentId = Guid.NewGuid().ToString(),
                    OrderId = request.OrderId,
                    Amount = order.TotalAmount,
                    PaymentDate = DateTime.UtcNow,
                    PaymentMethod = request.PaymentMethod,
                    Status = "Completed"
                };

                await paymentRepository.AddAsync(payment);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();

                return new PaymentResponseModel
                {
                    PaymentId = payment.PaymentId,
                    OrderId = payment.OrderId,
                    Amount = payment.Amount,
                    PaymentDate = payment.PaymentDate,
                    PaymentMethod = payment.PaymentMethod,
                    Status = payment.Status
                };
            }
            catch (Exception)
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task<PaymentResponseModel?> GetByOrderIdAsync(string orderId)
        {
            var paymentRepository = _unitOfWork.GetRepository<Payment>();
            var payment = await paymentRepository.Entities.FirstOrDefaultAsync(p => p.OrderId == orderId);

            if (payment == null) return null;

            return new PaymentResponseModel
            {
                PaymentId = payment.PaymentId,
                OrderId = payment.OrderId ?? string.Empty,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod,
                Status = payment.Status
            };
        }
    }
}
