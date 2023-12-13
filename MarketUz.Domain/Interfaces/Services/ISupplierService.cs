using DiyorMarket.Domain.DTOs.Supplier;
using DiyorMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface ISupplierService
    {
        IEnumerable<SupplierDto> GetSuppliers { get; }
        SupplierDto GetSupplierById(int id);
        SupplierDto CreateSupplier(SupplierForCreateDto supplier);
        void UpdateSupplier(SupplierForUpdateDto supplier);
        void DeleteSupplier(int id);
    }
}
