namespace MarketUz.Domain.DTOs.SaleItem
{
    public record SaleItemForUpdateDto(
        int Id,
         int Quantity,
         decimal UnitPrice,
         int ProductId,
         int SaleId);


}
