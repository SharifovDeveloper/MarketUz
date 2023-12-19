namespace MarketUz.Domain.DTOs.SaleItem
{
    public record SaleItemForCreateDto(
        int Quantity,
        decimal UnitPrice,
        int ProductId,
        int SaleId);


}
