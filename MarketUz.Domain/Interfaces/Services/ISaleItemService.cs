using MarketUz.Domain.DTOs.SaleItem;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface ISaleItemService
    {
        IEnumerable<SaleItemDto> GetSaleItems();
        SaleItemDto? GetSaleItemById(int id);
        SaleItemDto CreateSaleItem(SaleItemForCreateDto saleItemToCreate);
        void UpdateSaleItem(SaleItemForUpdateDto saleItemToUpdate);
        void DeleteSaleItem(int id);
    }
}
