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

        public async Task<OrderResponseModel> CreateAsync(CreateOrderRequestModel request)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var orderRepository = _unitOfWork.GetRepository<Order>();
                var productRepository = _unitOfWork.GetRepository<Product>();
                var orderDetailRepository = _unitOfWork.GetRepository<OrderDetail>();

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

                var order = new Order
                {
                    OrderId = Guid.NewGuid().ToString(),
                    UserId = request.UserId,
                    OrderDate = DateTime.UtcNow,
                    TotalAmount = totalAmount,
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
                OrderDetails = order.OrderDetails?.Select(od => new OrderDetailResponseModel
                {
                    OrderDetailId = od.OrderDetailId,
                    ProductId = od.ProductId,
                    ProductName = od.Product?.Name ?? string.Empty,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice,
                    Total = od.Quantity * od.UnitPrice
                }).ToList() ?? new List<OrderDetailResponseModel>(),
                Payment = order.Payment == null ? null : new PaymentResponseModel
                {
                    PaymentId = order.Payment.PaymentId,
                    OrderId = order.Payment.OrderId ?? string.Empty,
                    Amount = order.Payment.Amount,
                    PaymentDate = order.Payment.PaymentDate,
                    PaymentMethod = order.Payment.PaymentMethod,
                    Status = order.Payment.Status
                }
            };
        }

        public async Task<PagedResult<OrderResponseModel>> GetPagingAsync(OrderPagingRequestModel request)
        {
            var orderRepository = _unitOfWork.GetRepository<Order>();
            var query = orderRepository.Entities
                .Include(o => o.OrderDetails)
                .Include(o => o.Payment)
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
                    Payment = o.Payment == null ? null : new PaymentResponseModel
                    {
                        PaymentId = o.Payment.PaymentId,
                        Status = o.Payment.Status,
                        PaymentMethod = o.Payment.PaymentMethod
                    }
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

                if (order.Payment != null)
                {
                    throw new InvalidOperationException("Không thể xóa đơn hàng đã thanh toán.");
                }

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

        public async Task<OrderResponseModel> UpdateAsync(string id, CreateOrderRequestModel request)
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

                if (order.Payment != null)
                {
                    throw new InvalidOperationException("Không thể cập nhật đơn hàng đã thanh toán.");
                }

                var productRepository = _unitOfWork.GetRepository<Product>();
                var orderDetailRepository = _unitOfWork.GetRepository<OrderDetail>();

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

                // Update order
                order.UserId = request.UserId;
                order.TotalAmount = totalAmount;
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
