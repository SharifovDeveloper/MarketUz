using MarketUz.Domain.DTOs.SupplyItem;

namespace MarketUz.Domain.DTOs.Supply
{
    public record SupplyDto(
        int Id,
        DateTime SupplyDate,
        decimal TotalDue,
        int SupplierId,
        ICollection<SupplyItemDto> SupplyItems);
}
