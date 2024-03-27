using MarketUz.Domain.DTOs.Sale;

namespace MarketUz.Domain.DTOs.Customer
{
    public record CustomerDto(
        int Id,
        string FullName,
        string PhoneNumber,
        ICollection<SaleDto> Sales);
}
