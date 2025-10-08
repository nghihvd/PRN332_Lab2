using Microsoft.EntityFrameworkCore;
using PRN232.Lab2.CoffeeStore.Repositories.Interfaces.UOW;
using PRN232.Lab2.CoffeeStore.Repositories.Models;
using PRN232.Lab2.CoffeeStore.Services.Commons;
using PRN232.Lab2.CoffeeStore.Services.Interfaces;
using PRN232.Lab2.CoffeeStore.Services.Models.Requests.Category;
using PRN232.Lab2.CoffeeStore.Services.Models.Responses;

namespace PRN232.Lab2.CoffeeStore.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryResponseModel> CreateAsync(CreateCategoryRequestModel request)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var categoryRepository = _unitOfWork.GetRepository<Category>();
                var category = new Category
                {
                    CategoryId = Guid.NewGuid().ToString(),
                    Name = request.Name,
                    Description = request.Description,
                    CreatedDate = DateTime.UtcNow
                };
                await categoryRepository.AddAsync(category);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
                return (await GetByIdAsync(category.CategoryId))!;
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
                var categoryRepository = _unitOfWork.GetRepository<Category>();
                var category = await categoryRepository.Entities.Include(c => c.Products).FirstOrDefaultAsync(c => c.CategoryId == id);
                if (category == null)
                {
                    return false;
                }
                if (category.Products != null && category.Products.Any())
                {
                    throw new InvalidOperationException("Danh mục đã được sử dụng và không được xoá.");
                }
                await categoryRepository.DeleteAsync(category);
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

        public async Task<CategoryResponseModel?> GetByIdAsync(string id)
        {
            var categoryRepository = _unitOfWork.GetRepository<Category>();
            var category = await categoryRepository.Entities.Include(c => c.Products).FirstOrDefaultAsync(c => c.CategoryId == id);
            if (category == null)
            {
                return null;
            }
            return new CategoryResponseModel
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                CreatedDate = category.CreatedDate,
                ProductCount = category.Products?.Count ?? 0
            };
        }

        public async Task<PagedResult<CategoryResponseModel>> GetPagingAsync(CategoryPagingRequestModel request)
        {
            var categoryRepository = _unitOfWork.GetRepository<Category>();
            var query = categoryRepository.Entities.Include(c => c.Products).AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(c => c.Name.Contains(request.SearchTerm) || (c.Description != null && c.Description.Contains(request.SearchTerm)));
            }

            var totalCount = await query.CountAsync();

            query = request.SortBy.ToLower() switch
            {
                "name" => request.IsDescending ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name),
                "createddate" => request.IsDescending ? query.OrderByDescending(c => c.CreatedDate) : query.OrderBy(c => c.CreatedDate),
                _ => query.OrderBy(c => c.Name)
            };

            var categories = await query
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .Select(c => new CategoryResponseModel
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name,
                    Description = c.Description,
                    CreatedDate = c.CreatedDate,
                    ProductCount = c.Products != null ? c.Products.Count : 0
                })
                .ToListAsync();

            return new PagedResult<CategoryResponseModel>(categories, totalCount, request.PageIndex, request.PageSize);
        }

        public async Task<CategoryResponseModel> UpdateAsync(string id, UpdateCategoryRequestModel request)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var categoryRepository = _unitOfWork.GetRepository<Category>();
                var category = await categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    throw new KeyNotFoundException("Không tìm thấy danh mục.");
                }
                category.Name = request.Name;
                category.Description = request.Description;
                await categoryRepository.UpdateAsync(category);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
                return (await GetByIdAsync(category.CategoryId))!;
            }
            catch (Exception)
            {
                _unitOfWork.RollBack();
                throw;
            }
        }
    }
}
