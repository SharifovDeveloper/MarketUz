using MarketUz.Domain.DTOs.SaleItem;

namespace MarketUz.Domain.DTOs.Product
{
    public record ProductDto(
         int Id,
         string Name,
         string Description,
         decimal Price,
         DateTime ExpireDate,
         ICollection<SaleItemDto> SaleItems);
    
}
