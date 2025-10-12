using Microsoft.EntityFrameworkCore;
using PRN232.Lab2.CoffeeStore.Repositories.Interfaces.UOW;
using PRN232.Lab2.CoffeeStore.Repositories.Models;
using PRN232.Lab2.CoffeeStore.Services.Commons;
using PRN232.Lab2.CoffeeStore.Services.Interfaces;
using PRN232.Lab2.CoffeeStore.Services.Models.Requests.Order;
using PRN232.Lab2.CoffeeStore.Services.Models.Responses;

namespace PRN232.Lab2.CoffeeStore.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderResponseModel> CreateAsync(CreateOrderRequestModel request, string userId)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var orderRepository = _unitOfWork.GetRepository<Order>();
                var productRepository = _unitOfWork.GetRepository<Product>();
                var orderDetailRepository = _unitOfWork.GetRepository<OrderDetail>();
                var paymentRepository = _unitOfWork.GetRepository<Payment>();

                decimal totalAmount = 0;
                var orderDetails = new List<OrderDetail>();

                foreach (var detail in request.OrderDetails)
                {
                    var product = await productRepository.GetByIdAsync(detail.ProductId);
                    if (product == null || !product.IsActive)
                    {
                        throw new KeyNotFoundException($"Sản phẩm với ID {detail.ProductId} không hợp lệ hoặc không hoạt động.");
                    }

                    var orderDetail = new OrderDetail
                    {
                        OrderDetailId = Guid.NewGuid().ToString(),
                        ProductId = detail.ProductId,
                        Quantity = detail.Quantity,
                        UnitPrice = product.Price
                    };
                    orderDetails.Add(orderDetail);
                    totalAmount += orderDetail.Quantity * orderDetail.UnitPrice;
                }

                // Validate payment method if provided
                if (!string.IsNullOrEmpty(request.PaymentId))
                {
                    var paymentMethod = await paymentRepository.GetByIdAsync(request.PaymentId);
                    if (paymentMethod == null)
                    {
                        throw new KeyNotFoundException("Phương thức thanh toán không hợp lệ.");
                    }
                }

                var order = new Order
                {
                    OrderId = Guid.NewGuid().ToString(),
                    UserId = userId,
                    OrderDate = DateTime.UtcNow,
                    TotalAmount = totalAmount,
                    PaymentId = request.PaymentId,
                    OrderDetails = orderDetails
                };

                await orderRepository.AddAsync(order);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();

                return (await GetByIdAsync(order.OrderId))!;
            }
            catch (Exception)
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task<OrderResponseModel?> GetByIdAsync(string id)
        {
            var orderRepository = _unitOfWork.GetRepository<Order>();
            var order = await orderRepository.Entities
                .Include(o => o.OrderDetails!)
                    .ThenInclude(od => od.Product)
                .Include(o => o.Payment)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null) return null;

            return new OrderResponseModel
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                PaymentId = order.PaymentId,
                OrderDetails = order.OrderDetails?.Select(od => new OrderDetailResponseModel
                {
                    OrderDetailId = od.OrderDetailId,
                    ProductId = od.ProductId,
                    ProductName = od.Product?.Name ?? string.Empty,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice,
                    Total = od.Quantity * od.UnitPrice
                }).ToList() ?? new List<OrderDetailResponseModel>(),
                Payment = null // Order now references payment method by PaymentId; no transaction details here
            };
        }

        public async Task<PagedResult<OrderResponseModel>> GetPagingAsync(OrderPagingRequestModel request)
        {
            var orderRepository = _unitOfWork.GetRepository<Order>();
            var query = orderRepository.Entities
                .Include(o => o.OrderDetails)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.UserId))
            {
                query = query.Where(o => o.UserId == request.UserId);
            }
            if (request.FromDate.HasValue)
            {
                query = query.Where(o => o.OrderDate >= request.FromDate.Value);
            }
            if (request.ToDate.HasValue)
            {
                query = query.Where(o => o.OrderDate <= request.ToDate.Value);
            }

            var totalCount = await query.CountAsync();

            query = request.SortBy.ToLower() switch
            {
                "orderdate" => request.IsDescending ? query.OrderByDescending(o => o.OrderDate) : query.OrderBy(o => o.OrderDate),
                "totalamount" => request.IsDescending ? query.OrderByDescending(o => o.TotalAmount) : query.OrderBy(o => o.TotalAmount),
                _ => query.OrderByDescending(o => o.OrderDate)
            };

            var orders = await query
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .Select(o => new OrderResponseModel
                {
                    OrderId = o.OrderId,
                    UserId = o.UserId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    PaymentId = o.PaymentId
                })
                .ToListAsync();

            return new PagedResult<OrderResponseModel>(orders, totalCount, request.PageIndex, request.PageSize);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var orderRepository = _unitOfWork.GetRepository<Order>();
                var order = await orderRepository.GetByIdAsync(id);
                if (order == null)
                {
                    return false;
                }

                // No longer blocking delete based on payment; Order now references a payment method

                var orderDetailRepository = _unitOfWork.GetRepository<OrderDetail>();
                var orderDetails = await orderDetailRepository.Entities
                    .Where(od => od.OrderId == id)
                    .ToListAsync();

                foreach (var detail in orderDetails)
                {
                    await orderDetailRepository.DeleteAsync(detail);
                }

                await orderRepository.DeleteAsync(order);
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

        public async Task<OrderResponseModel> UpdateAsync(string id, CreateOrderRequestModel request, string userId)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var orderRepository = _unitOfWork.GetRepository<Order>();
                var order = await orderRepository.GetByIdAsync(id);
                if (order == null)
                {
                    throw new KeyNotFoundException("Không tìm thấy đơn hàng.");
                }

                // No longer blocking update based on payment; Order now references a payment method

                var productRepository = _unitOfWork.GetRepository<Product>();
                var orderDetailRepository = _unitOfWork.GetRepository<OrderDetail>();
                var paymentRepository = _unitOfWork.GetRepository<Payment>();

                // Delete existing order details
                var existingDetails = await orderDetailRepository.Entities
                    .Where(od => od.OrderId == id)
                    .ToListAsync();
                foreach (var detail in existingDetails)
                {
                    await orderDetailRepository.DeleteAsync(detail);
                }

                // Create new order details
                decimal totalAmount = 0;
                var orderDetails = new List<OrderDetail>();

                foreach (var detail in request.OrderDetails)
                {
                    var product = await productRepository.GetByIdAsync(detail.ProductId);
                    if (product == null || !product.IsActive)
                    {
                        throw new KeyNotFoundException($"Sản phẩm với ID {detail.ProductId} không hợp lệ hoặc không hoạt động.");
                    }

                    var orderDetail = new OrderDetail
                    {
                        OrderDetailId = Guid.NewGuid().ToString(),
                        OrderId = id,
                        ProductId = detail.ProductId,
                        Quantity = detail.Quantity,
                        UnitPrice = product.Price
                    };
                    orderDetails.Add(orderDetail);
                    totalAmount += orderDetail.Quantity * orderDetail.UnitPrice;
                }

                // Validate payment method if provided
                if (!string.IsNullOrEmpty(request.PaymentId))
                {
                    var paymentMethod = await paymentRepository.GetByIdAsync(request.PaymentId);
                    if (paymentMethod == null)
                    {
                        throw new KeyNotFoundException("Phương thức thanh toán không hợp lệ.");
                    }
                }

                // Update order
                order.UserId = userId;
                order.TotalAmount = totalAmount;
                order.PaymentId = request.PaymentId;
                order.OrderDetails = orderDetails;

                await orderRepository.UpdateAsync(order);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();

                return (await GetByIdAsync(id))!;
            }
            catch (Exception)
            {
                _unitOfWork.RollBack();
                throw;
            }
        }
    }
}
