

using DiyorMarket.Domain.DTOs.Sale;
using MarketUz.Domain.DTOs.Sale;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface ISaleService
    {
        IEnumerable<SaleDto> GetSales();
        SaleDto GetSale(int id);
        SaleDto CreateSale(SaleForCreateDto sale);
        void UpdateSale(SaleForUpdateDto sale);
        void DeleteSale(int id);
    }
}
