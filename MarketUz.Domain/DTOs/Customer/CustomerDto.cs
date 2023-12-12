using DiyorMarket.Domain.DTOs.Sale;
using MarketUz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketUz.Domain.DTOs.Customer
{
    public record CustomerDto(
        int Id,
        string FullName,
        string Phone,
        ICollection<SaleDto> Sales);
   
}
