

using DiyorMarket.Domain.DTOs.Sale;
using MarketUz.Domain.DTOs.Sale;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface ISaleService
    {
        IEnumerable<SaleDto> GetSales();
        SaleDto? GetSaleById(int id);
        SaleDto CreateSale(SaleForCreateDto saleToCreate);
        void UpdateSale(SaleForUpdateDto saleToUpdate);
        void DeleteSale(int id);
    }
}
