namespace MarketUz.Domain.DTOs.SupplyItem
{
    public record SupplyItemDto(
          int Id,
          int Quantity,
          decimal UnitPrice,
          int ProductId,
          int SupplyId);
}
