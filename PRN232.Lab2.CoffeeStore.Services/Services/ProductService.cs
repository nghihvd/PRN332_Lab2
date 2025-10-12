using Microsoft.EntityFrameworkCore;
using PRN232.Lab2.CoffeeStore.Repositories.Interfaces.UOW;
using PRN232.Lab2.CoffeeStore.Repositories.Models;
using PRN232.Lab2.CoffeeStore.Services.Commons;
using PRN232.Lab2.CoffeeStore.Services.Interfaces;
using PRN232.Lab2.CoffeeStore.Services.Models.Requests.Product;
using PRN232.Lab2.CoffeeStore.Services.Models.Responses;
using System.Linq.Expressions;

namespace PRN232.Lab2.CoffeeStore.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductResponseModel?> GetByIdAsync(string productId)
        {
            var productRepository = _unitOfWork.GetRepository<Product>();
            var product = await productRepository.Entities
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
                return null;

            return new ProductResponseModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsActive = product.IsActive,
                CreatedDate = product.CreatedDate,
                LastModifiedDate = product.LastModifiedDate,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name
            };
        }

        public async Task<PagedResult<ProductResponseModel>> GetPagingAsync(ProductPagingRequestModel request)
        {
            var productRepository = _unitOfWork.GetRepository<Product>();
            var query = productRepository.Entities.Include(p => p.Category).AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(p => p.Name.Contains(request.SearchTerm) || (p.Description != null && p.Description.Contains(request.SearchTerm)));
            }
            if (!string.IsNullOrEmpty(request.CategoryId))
            {
                query = query.Where(p => p.CategoryId == request.CategoryId);
            }
            if (request.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= request.MinPrice.Value);
            }
            if (request.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= request.MaxPrice.Value);
            }
            if (request.IsActive.HasValue)
            {
                query = query.Where(p => p.IsActive == request.IsActive.Value);
            }

            var totalCount = await query.CountAsync();

            // Sorting
            query = request.SortBy.ToLower() switch
            {
                "name" => request.IsDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                "price" => request.IsDescending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                "createddate" => request.IsDescending ? query.OrderByDescending(p => p.CreatedDate) : query.OrderBy(p => p.CreatedDate),
                _ => query.OrderBy(p => p.Name)
            };

            // Pagination
            var products = await query
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .Select(p => new ProductResponseModel
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    IsActive = p.IsActive,
                    CreatedDate = p.CreatedDate,
                    LastModifiedDate = p.LastModifiedDate,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null ? p.Category.Name : null
                })
                .ToListAsync();

            return new PagedResult<ProductResponseModel>(products, totalCount, request.PageIndex, request.PageSize);
        }

        public async Task<ProductResponseModel> CreateAsync(CreateProductRequestModel productCreateDto)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                // Kiểm tra CategoryId có tồn tại không
                if (!string.IsNullOrEmpty(productCreateDto.CategoryId))
                {
                    var categoryRepository = _unitOfWork.GetRepository<Category>();
                    var category = await categoryRepository.GetByIdAsync(productCreateDto.CategoryId);
                    if (category == null)
                    {
                        throw new KeyNotFoundException($"Danh mục với ID {productCreateDto.CategoryId} không tồn tại.");
                    }
                }

                var productRepository = _unitOfWork.GetRepository<Product>();
                var product = new Product
                {
                    ProductId = Guid.NewGuid().ToString(),
                    Name = productCreateDto.Name,
                    Description = productCreateDto.Description,
                    Price = productCreateDto.Price,
                    CategoryId = productCreateDto.CategoryId,
                    IsActive = productCreateDto.IsActive,
                    CreatedDate = DateTime.UtcNow
                };

                await productRepository.AddAsync(product);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();

                return (await GetByIdAsync(product.ProductId))!;
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task<ProductResponseModel> UpdateAsync(string id,UpdateProductRequestModel productUpdateDto)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var productRepository = _unitOfWork.GetRepository<Product>();
                var existingProduct = await productRepository.GetByIdAsync(id);

                if (existingProduct == null)
                {
                    throw new KeyNotFoundException($"Không tìm thấy sản phẩm với ID {id}.");
                }

                // Kiểm tra CategoryId có tồn tại không
                if (!string.IsNullOrEmpty(productUpdateDto.CategoryId))
                {
                    var categoryRepository = _unitOfWork.GetRepository<Category>();
                    var category = await categoryRepository.GetByIdAsync(productUpdateDto.CategoryId);
                    if (category == null)
                    {
                        throw new KeyNotFoundException($"Danh mục với ID {productUpdateDto.CategoryId} không tồn tại.");
                    }
                }

                existingProduct.Name = productUpdateDto.Name;
                existingProduct.Description = productUpdateDto.Description;
                existingProduct.Price = productUpdateDto.Price;
                existingProduct.CategoryId = productUpdateDto.CategoryId;
                existingProduct.IsActive = productUpdateDto.IsActive;
                existingProduct.LastModifiedDate = DateTime.UtcNow;

                await productRepository.UpdateAsync(existingProduct);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();

                return (await GetByIdAsync(existingProduct.ProductId))!;
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string productId)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var productRepository = _unitOfWork.GetRepository<Product>();
                var product = await productRepository.Entities.Include(p => p.OrderDetails).FirstOrDefaultAsync(p => p.ProductId == productId);

                if (product == null)
                {
                    return false; // Or throw KeyNotFoundException
                }

                if (product.OrderDetails != null && product.OrderDetails.Any())
                {
                    throw new InvalidOperationException("Sản phẩm đã được sử dụng và không được xoá.");
                }

                await productRepository.DeleteAsync(product);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();

                return true;
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }
    }
}
