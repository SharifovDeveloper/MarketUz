using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketUz.Domain.DTOs.SaleItem
{
    public record SaleItemDto(
      int Id,
      int Quantity,
      decimal UnitPrice,
      int ProductId,
      int SaleId);
    
}
