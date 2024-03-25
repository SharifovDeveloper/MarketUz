using MarketUz.Domain.DTOs.Category;
using MarketUz.Domain.Pagination;
using MarketUz.Domain.ResourceParameters;

namespace MarketUz.Domain.Interfaces.Services
{
    public interface ICategoryService
    {
        PaginatedList<CategoryDto> GetCategories(CategoryResourceParameters categoryResourceParameters);
        CategoryDto GetCategoryById(int id);
        CategoryDto CreateCategory(CategoryForCreateDto category);
        void UpdateCategory(CategoryForUpdateDto category);
        void DeleteCategory(int id);
    }
}
