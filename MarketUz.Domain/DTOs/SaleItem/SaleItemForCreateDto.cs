using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketUz.Domain.DTOs.SaleItem
{
    public record SaleItemForCreateDto(
        int Quantity,
        decimal UnitPrice,
        int ProductId,
        int SaleId);
        
    
}
