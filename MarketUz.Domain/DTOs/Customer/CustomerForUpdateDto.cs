namespace MarketUz.Domain.DTOs.Customer
{
    public record CustomerForUpdateDto(
        int Id,
        string FirstName,
        string LastName,
        string PhoneNumber);

}
