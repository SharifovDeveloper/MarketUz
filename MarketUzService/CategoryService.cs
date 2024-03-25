using AutoMapper;
using MarketUz.Domain.DTOs.Category;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Exceptions;
using MarketUz.Domain.Interfaces.Services;
using MarketUz.Domain.Pagination;
using MarketUz.Domain.ResourceParameters;
using MarketUz.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace MarketUz.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly MarketUzDbContext _context;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(IMapper mapper,
            ILogger<CategoryService> logger,
            MarketUzDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public PaginatedList<CategoryDto> GetCategories(CategoryResourceParameters categoryResourceParameters)
        {
            var query = _context.Categories.AsQueryable();
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
                    _ => throw new NotImplementedException()
                };

            }
            var categories = query.ToPaginatedList(categoryResourceParameters.PageSize, categoryResourceParameters.PageNumber);

            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);

            return new PaginatedList<CategoryDto>(categoryDtos, categories.TotalCount, categories.CurrentPage, categories.PageSize);
        }

        public CategoryDto? GetCategoryById(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);

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

        public void UpdateCategory(CategoryForUpdateDto categoryToUpdate)
        {
            var categoryEntity = _mapper.Map<Category>(categoryToUpdate);

            _context.Categories.Update(categoryEntity);
            _context.SaveChanges();
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
