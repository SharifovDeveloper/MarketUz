namespace MarketUz.Domain.DTOs.SaleItem
{
    public record SaleItemForUpdateDto(
         int Quantity,
         decimal UnitPrice,
         int ProductId,
         int SaleId);


}
