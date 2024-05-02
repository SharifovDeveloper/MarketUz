using AutoMapper;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Exceptions;
using MarketUz.Domain.Interfaces.Services;
using MarketUz.Domain.Pagination;
using MarketUz.Domain.ResourceParameters;
using MarketUz.Domain.Responses;
using MarketUz.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Inflow.Core.Category;

namespace MarketUz.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly MarketUzDbContext _context;

        public CategoryService(IMapper mapper,
            MarketUzDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public GetBaseResponse<CategoryDto> GetCategories(CategoryResourceParameters categoryResourceParameters)
        {
            var query = _context.Categories.Include(s => s.Products).IgnoreAutoIncludes().AsQueryable();

            if (!string.IsNullOrWhiteSpace(categoryResourceParameters.SearchString))
            {
                query = query.Where(x => x.Name.Contains(categoryResourceParameters.SearchString));
            }

            if (!string.IsNullOrEmpty(categoryResourceParameters.OrderBy))
            {
                query = categoryResourceParameters.OrderBy.ToLowerInvariant() switch
                {
                    "name" => query.OrderBy(x => x.Name),
                    "namedesc" => query.OrderByDescending(x => x.Name),
                    _ => query.OrderBy(x => x.Name),
                };
            }

            var categories = query.ToPaginatedList(categoryResourceParameters.PageSize, categoryResourceParameters.PageNumber);
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            var paginatedResult = new PaginatedList<CategoryDto>(categoryDtos, categories.TotalCount, categories.CurrentPage, categories.PageSize);

            return paginatedResult.ToResponse();
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var categories = _context.Categories.Include(s => s.Products).ToList();

            return _mapper.Map<IEnumerable<CategoryDto>>(categories) ?? Enumerable.Empty<CategoryDto>();
        }

        public CategoryDto? GetCategoryById(int id)
        {
            var category = _context.Categories
                .Include(c => c.Products)
                .IgnoreAutoIncludes()
                .FirstOrDefault(x => x.Id == id);

            if (category is null)
            {
                throw new EntityNotFoundException($"Category with id: {id} not found");
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }

        public CategoryDto CreateCategory(CategoryForCreateDto categoryToCreate)
        {
            var categoryEntity = _mapper.Map<Category>(categoryToCreate);

            _context.Categories.Add(categoryEntity);

            _context.SaveChanges();

            var categoryDto = _mapper.Map<CategoryDto>(categoryEntity);

            return categoryDto;
        }

        public CategoryDto UpdateCategory(CategoryForUpdateDto categoryToUpdate)
        {
            var categoryEntity = _mapper.Map<Category>(categoryToUpdate);

            _context.Categories.Update(categoryEntity);
            _context.SaveChanges();

            var updatedCategoryDto = _mapper.Map<CategoryDto>(categoryEntity);

            return updatedCategoryDto;
        }

        public void DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category is not null)
            {
                _context.Categories.Remove(category);
            }

            _context.SaveChanges();
        }
    }
}
