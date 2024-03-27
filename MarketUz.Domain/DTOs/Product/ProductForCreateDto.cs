namespace MarketUz.Domain.DTOs.Product
{
    public record ProductForCreateDto(
       string Name,
       string Description,
       decimal SalePrice,
       decimal SupplyPrice,
       DateTime ExpireDate,
       int CategoryId);
}
