namespace MarketUz.Domain.DTOs.Product
{
    public record ProductForCreateDto(
         string Name,
         decimal Price,
         int CategoryId);

}
