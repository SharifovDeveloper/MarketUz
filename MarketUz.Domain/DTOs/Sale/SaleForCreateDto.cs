namespace MarketUz.Domain.DTOs.Sale
{
    public record SaleForCreateDto(
        DateTime saleDate,
        int CustomerId);
}
