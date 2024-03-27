using MarketUz.Domain.DTOs.SaleItem;

namespace MarketUz.Domain.DTOs.Sale
{
    public record SaleDto(
      int Id,
      DateTime SaleDate,
      int CustomerId,
      decimal TotalDue,
      ICollection<SaleItemDto> SaleItems);
}
