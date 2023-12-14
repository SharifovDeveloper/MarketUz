using AutoMapper;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Category;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiyorMarket.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICommonRepository _repository;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(IMapper mapper, ICommonRepository repository, ILogger<CategoryService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            try
            {
                var categories = _repository.Category.FindAll();
                var categoryDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);

                return categoryDto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Database error fetching category ", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching category ", ex);
                throw;
            }
        }
        public CategoryDto GetCategoryById(int id)
        {
            try
            {
                var categories = _repository.Category.FindById(id);
                var categoryDto = _mapper.Map<CategoryDto>(categories);

                return categoryDto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error fetching category with id : {id}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching category with id : {id}", ex);
                throw;
            }
        }
        public CategoryDto CreateCategory(CategoryForCreateDto category)
        {
            try
            {
                var categoryEntity = _mapper.Map<Category>(category);
                _repository.Category.Create(categoryEntity);

                _repository.SaveChanges();

                var categoryDto = _mapper.Map<CategoryDto>(categoryEntity);
                return categoryDto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Database error creating new category ", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating new category ", ex);
                throw;
            }
        }
        public void UpdateCategory(CategoryForUpdateDto category)
        {
            try
            {
                var categoryEntity = _mapper.Map<Category>(category);
                _repository.Category.Update(categoryEntity);

                _repository.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error updating category with id : {category.Id}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating category with id : {category.Id}", ex);
                throw;
            }
        }
        public void DeleteCategory(int id)
        {
            try
            {
                _repository.Category.Delete(id);
                _repository.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error deleting category with id : {id}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting category with id : {id}", ex);
                throw;
            }
        }
    }
}
