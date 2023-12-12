using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketUz.Domain.DTOs.Sale
{
    public record SaleForUpdateDto(
        int Id,
        DateTime saleDate,
        int CustomerId);


}
