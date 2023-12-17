using MarketUz.Domain.DTOs.SupplyItem;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface ISupplyItemService
    {
        IEnumerable<SupplyItemDto> GetSupplyItems();
        SupplyItemDto? GetSupplyItemById(int id);
        SupplyItemDto CreateSupplyItem(SupplyItemForCreateDto supplyItemToCreate);
        void UpdateSupplyItem(SupplyItemForUpdateDto supplyItemToUpdate);
        void DeleteSupplyItem(int id);
    }
}
