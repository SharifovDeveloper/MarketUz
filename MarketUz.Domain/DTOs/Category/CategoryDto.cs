using MarketUz.Domain.DTOs.Product;

namespace MarketUz.Domain.DTOs.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfProducts { get; set; }
        public ICollection<ProductDto> Products { get; set; }
    }
}
