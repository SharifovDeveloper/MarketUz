

using DiyorMarket.Domain.DTOs.SaleItem;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface ISaleItemService
    {
        IEnumerable<SaleItemDto> GetSaleItems();
        SaleItemDto GetSaleItem(int id);
        SaleItemDto CreatSaleItem(SaleItemForCreateDto saleItem);
        void UpdateSaleItem(SaleItemForUpdateDto saleItem);
        void DeleteSaleItem(int id);
    }
}
