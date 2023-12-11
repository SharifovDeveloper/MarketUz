using MarketUz.Domain.DTOs.Product;

namespace MarketUz.Domain.DTOs.Category
{
    public record CategoryDto(
        int Id,
        string Name,
        int NumberOfProducts,
        ICollection<ProductDto> Products);
}
