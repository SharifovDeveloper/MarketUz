using MarketUz.Domain.DTOs.SupplyItem;

namespace MarketUz.Domain.DTOs.Supply
{
    public record SupplyForCreateDto(
        DateTime SupplyDate,
        int SupplierId,
        ICollection<SupplyItemForCreateDto> SupplyItems);
}
