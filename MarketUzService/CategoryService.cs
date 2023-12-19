using AutoMapper;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Category;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Exceptions;
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

        public IEnumerable<CategoryDto> GetCategories()
        {
            var categories = _context.Categories.ToList();

            var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return categoryDtos;
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
