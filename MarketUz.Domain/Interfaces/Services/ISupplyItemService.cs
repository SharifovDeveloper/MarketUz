using MarketUz.Domain.DTOs.SupplyItem;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface ISupplyItemService
    {
        IEnumerable<SupplyItemDto> GetSupplyItems();
        SupplyItemDto GetSupplyItem(int id);
        SupplyItemDto CreateSupplyItem(SupplyItemForCreateDto createDto);
        void UpdateSupplyItem(SupplyItemForUpdateDto updateDto);
        void DeleteSupplyItem(int id);
    }
}
