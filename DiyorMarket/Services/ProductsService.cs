using DiyorMarket.Domain.Entities;

namespace DiyorMarketApi.Services
{
    public class ProductsService
    {
        private static List<Product> Products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Coca-Cola",
                Price = 2500,
                CategoryId = 1
            },
            new Product
            {
                Id = 2,
                Name = "Fanta",
                Price = 2500,
                CategoryId = 1
            },
            new Product
            {
                Id = 3,
                Name = "Sprite",
                Price = 2450,
                CategoryId = 1
            },
            new Product
            {
                Id = 4,
                Name = "Snikers",
                Price = 3500,
                CategoryId = 2
            },
            new Product
            {
                Id = 5,
                Name = "Mars",
                Price = 3200,
                CategoryId = 2
            },
        };

        public static List<Product> GetProducts()
            => Products;

        public static Product? GetProduct(int id)
            => Products.FirstOrDefault(x => x.Id == id);

        public static void Create(Product product)
            => Products.Add(product);

        public static void Update(Product product)
        {
            var productToUpdate = Products.FirstOrDefault(x => x.Id == product.Id);

            if (productToUpdate is null)
            {
                return;
            }

            productToUpdate.Name = product.Name;
            productToUpdate.Price = product.Price;
            productToUpdate.CategoryId = product.CategoryId;
        }

        public static void Delete(int id)
        {
            var product = Products.FirstOrDefault(x => x.Id == id);

            if (product is not null)
            {
                Products.Remove(product);
            }
        }
    }
}
