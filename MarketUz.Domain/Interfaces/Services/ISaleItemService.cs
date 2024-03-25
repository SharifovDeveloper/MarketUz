using MarketUz.Domain.DTOs.SaleItem;

namespace MarketUz.Domain.Interfaces.Services
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