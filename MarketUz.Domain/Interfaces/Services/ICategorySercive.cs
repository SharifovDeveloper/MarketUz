using MarketUz.Domain.DTOs.Category;
using MarketUz.Domain.Pagination;
using MarketUz.Domain.ResourceParameters;

namespace MarketUz.Domain.Interfaces.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAllCategories();
        GetBaseResponse<CategoryDto> GetCategories(CategoryResourceParameters categoryResourceParameters);
        CategoryDto? GetCategoryById(int id);
        CategoryDto CreateCategory(CategoryForCreateDto categoryToCreate);
        CategoryDto UpdateCategory(CategoryForUpdateDto categoryToUpdate);
        void DeleteCategory(int id);
    }
}
