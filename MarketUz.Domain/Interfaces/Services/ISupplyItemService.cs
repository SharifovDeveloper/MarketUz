using MarketUz.Domain.DTOs.SupplyItem;
using MarketUz.Domain.ResourceParameters;
using MarketUz.Domain.Responses;

namespace MarketUz.Domain.Interfaces.Services
{
    public interface ISupplyItemService
    {
        IEnumerable<SupplyItemDto> GetAllSupplyItems();
        GetBaseResponse<SupplyItemDto> GetSupplyItems(SupplyItemResourceParameters supplyItemResourceParameters);
        SupplyItemDto? GetSupplyItemById(int id);
        SupplyItemDto CreateSupplyItem(SupplyItemForCreateDto supplyItemToCreate);
        void UpdateSupplyItem(SupplyItemForUpdateDto supplyItemToUpdate);
        void DeleteSupplyItem(int id);
    }
}
