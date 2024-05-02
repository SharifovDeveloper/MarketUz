using MarketUz.Domain.ResourceParameters;
using MarketUz.Domain.Responses;
using Inflow.Core.Supplier;

namespace MarketUz.Domain.Interfaces.Services
{
    public interface ISupplierService
    {
        IEnumerable<SupplierDto> GetAllSuppliers();
        GetBaseResponse<SupplierDto> GetSuppliers(SupplierResourceParameters supplierResourceParameters);
        SupplierDto? GetSupplierById(int id);
        SupplierDto CreateSupplier(SupplierForCreateDto supplierToCreate);
        SupplierDto UpdateSupplier(SupplierForUpdateDto supplierToUpdate);
        void DeleteSupplier(int id);
    }
}
