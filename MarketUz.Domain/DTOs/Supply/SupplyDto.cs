using MarketUz.Domain.DTOs.SupplyItem;

namespace MarketUz.Domain.DTOs.Supply
{
    public record SupplyDto(
        int Id,
        DateTime SupplyDate,
        int SupplierId,
        ICollection<SupplyItemDto> Sales);


}
