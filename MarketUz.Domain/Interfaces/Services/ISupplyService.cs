using MarketUz.Domain.DTOs.Supply;

namespace MarketUz.Domain.Interfaces.Services
{
    public interface ISupplyService
    {
        IEnumerable<SupplyDto> GetSupplies();
        SupplyDto GetSupplyById(int id);
        SupplyDto CreateSupply(SupplyForCreateDto supply);
        void UpdateSupply(SupplyForUpdateDto supply);
        void DeleteSupply(int id);
    }
}
