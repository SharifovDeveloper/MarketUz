using DiyorMarket.Domain.Entities;
namespace DiyorMarketApi.Services
{
    public class CategoriesService
    {
        public static List<Category> Categories = new List<Category>
        {
            new Category
            {
                Id = 1,
                Name = "Drinks"
            },
            new Category
            {
                Id = 2,
                Name = "Chocolate"
            },
            new Category
            {
                Id = 3,
                Name = "Fruits"
            },
        };

        public static List<Category> GetCategories()
            => Categories;

        public static Category? GetCategory(int id)
            => Categories.FirstOrDefault(x => x.Id == id);

        public static void Create(Category category)
            => Categories.Add(category);

        public static void Update(Category categoryToUpdate)
        {
            var category = Categories.FirstOrDefault(x => x.Id == categoryToUpdate.Id);

            if (category is null)
            {
                return;
            }

            category.Name = categoryToUpdate.Name;
        }

        public static void Delete(int id)
        {
            var category = Categories.FirstOrDefault(x => x.Id == id);

            if (category is null)
            {
                return;
            }

            Categories.Remove(category);
        }
    }
}
