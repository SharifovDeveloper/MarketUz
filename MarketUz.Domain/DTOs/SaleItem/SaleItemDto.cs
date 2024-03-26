using MarketUz.Domain.DTOs.Product;

namespace MarketUz.Domain.DTOs.SaleItem
{
    public record SaleItemDto(
      int Id,
      string ProductName,
      int Quantity,
      decimal UnitPrice,
      ProductDto Product,
      int SaleId,
      decimal TotalDue);
}
