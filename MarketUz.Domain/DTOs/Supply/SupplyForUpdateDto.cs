using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketUz.Domain.DTOs.Supply
{
    public record SupplyForUpdateDto(
        int Id,
        DateTime SupplyDate,
        int SupplierId);
   
}
