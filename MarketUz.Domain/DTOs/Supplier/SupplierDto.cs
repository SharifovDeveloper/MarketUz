using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketUz.Domain.DTOs.Supplier
{
    public record SupplierDto(
        int Id,
        string FullName,
        string Phone
        );
    
}
