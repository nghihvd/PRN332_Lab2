using Microsoft.EntityFrameworkCore;
using PRN232.Lab2.CoffeeStore.Repositories.Interfaces.UOW;
using PRN232.Lab2.CoffeeStore.Repositories.Models;
using PRN232.Lab2.CoffeeStore.Services.Commons;
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

        public async Task<PaymentResponseModel> CreateAsync(CreatePaymentRequestModel request)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var paymentRepository = _unitOfWork.GetRepository<Payment>();
                var payment = new Payment
                {
                    PaymentId = Guid.NewGuid().ToString(),
                    PaymentMethod = request.PaymentMethod,
                    Status = request.Status
                };

                await paymentRepository.AddAsync(payment);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();

                return new PaymentResponseModel
                {
                    PaymentId = payment.PaymentId,
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

        // Removed GetByOrderIdAsync; Payment is now a catalog

        public async Task<PaymentResponseModel?> GetByIdAsync(string id)
        {
            var repo = _unitOfWork.GetRepository<Payment>();
            var payment = await repo.Entities
                .FirstOrDefaultAsync(p => p.PaymentId == id);

            if (payment == null) return null;

            return new PaymentResponseModel
            {
                PaymentId = payment.PaymentId,
                PaymentMethod = payment.PaymentMethod,
                Status = payment.Status
            };
        }

        public async Task<PagedResult<PaymentResponseModel>> GetPagingAsync(PaymentPagingRequestModel request)
        {
            var repo = _unitOfWork.GetRepository<Payment>();
            var query = repo.Entities.AsQueryable();
            if (!string.IsNullOrEmpty(request.Status))
            {
                query = query.Where(p => p.Status == request.Status);
            }

            var totalCount = await query.CountAsync();

            switch (request.SortBy.ToLower())
            {
                case "status":
                    query = request.IsDescending ? query.OrderByDescending(p => p.Status) : query.OrderBy(p => p.Status);
                    break;
                case "paymentmethod":
                default:
                    query = request.IsDescending ? query.OrderByDescending(p => p.PaymentMethod) : query.OrderBy(p => p.PaymentMethod);
                    break;
            }

            var items = await query
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .Select(p => new PaymentResponseModel
                {
                    PaymentId = p.PaymentId,
                    PaymentMethod = p.PaymentMethod,
                    Status = p.Status
                })
                .ToListAsync();

            return new PagedResult<PaymentResponseModel>(items, totalCount, request.PageIndex, request.PageSize);
        }

        public async Task<PaymentResponseModel> UpdateAsync(string id, UpdatePaymentRequestModel request)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var repo = _unitOfWork.GetRepository<Payment>();
                var payment = await repo.GetByIdAsync(id);
                if (payment == null)
                {
                    throw new KeyNotFoundException("Không tìm thấy thanh toán.");
                }

                payment.PaymentMethod = request.PaymentMethod;
                payment.Status = request.Status;

                await repo.UpdateAsync(payment);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();

                return new PaymentResponseModel
                {
                    PaymentId = payment.PaymentId,
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

        public async Task<bool> DeleteAsync(string id)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var repo = _unitOfWork.GetRepository<Payment>();
                var payment = await repo.GetByIdAsync(id);
                if (payment == null)
                {
                    return false;
                }

                await repo.DeleteAsync(payment);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception)
            {
                _unitOfWork.RollBack();
                throw;
            }
        }
    }
}
