namespace MarketUz.Domain.DTOs.SupplyItem
{
    public record SupplyItemForUpdateDto(
        int Id,
        int Quantity,
        decimal UnitPrice,
        int ProductId,
        int SupplyId);

}
