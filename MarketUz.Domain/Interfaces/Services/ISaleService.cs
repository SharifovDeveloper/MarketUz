using MarketUz.Domain.DTOs.Sale;
using MarketUz.Domain.ResourceParameters;
using MarketUz.Domain.Responses;

namespace MarketUz.Domain.Interfaces.Services
{
    public interface ISaleService
    {
        IEnumerable<SaleDto> GetAllSales();
        IEnumerable<SaleDto> GetCustomersSale(int customersId);
        GetBaseResponse<SaleDto> GetSales(SaleResourceParameters saleResourceParameters);
        SaleDto? GetSaleById(int id);
        SaleDto CreateSale(SaleForCreateDto saleToCreate);
        void UpdateSale(SaleForUpdateDto saleToUpdate);
        void DeleteSale(int id);
    }
}
