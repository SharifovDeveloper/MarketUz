using MarketUz.Domain.DTOs.Supplier;

namespace MarketUz.Domain.Interfaces.Services
{
    public interface ISupplierService
    {
        IEnumerable<SupplierDto> GetSuppliers();
        SupplierDto GetSupplierById(int id);
        SupplierDto CreateSupplier(SupplierForCreateDto supplier);
        void UpdateSupplier(SupplierForUpdateDto supplier);
        void DeleteSupplier(int id);
    }
}
