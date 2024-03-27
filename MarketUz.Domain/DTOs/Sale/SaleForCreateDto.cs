using MarketUz.Domain.DTOs.SaleItem;

namespace MarketUz.Domain.DTOs.Sale
{
    public record SaleForCreateDto(
       DateTime SaleDate,
       int CustomerId,
       ICollection<SaleItemForCreateDto> SaleItems);
}
