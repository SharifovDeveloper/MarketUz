using MarketUz.Domain.ResourceParameters;
using MarketUz.Domain.Responses;
using Inflow.Core.Supply;

namespace MarketUz.Domain.Interfaces.Services
{
    public interface ISupplyService
    {
        IEnumerable<SupplyDto> GetAllSupplies();
        GetBaseResponse<SupplyDto> GetSupplies(SupplyResourceParameters supplyResourceParameters);
        SupplyDto? GetSupplyById(int id);
        SupplyDto CreateSupply(SupplyForCreateDto supplyToCreate);
        void UpdateSupply(SupplyForUpdateDto supplyToUpdate);
        void DeleteSupply(int id);
    }
}
