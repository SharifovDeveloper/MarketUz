using DiyorMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiyorMarket.Domain.DTOs.Customer
{
    public record CustomerDto(
        string FullName,
        string Phone,
        ICollection<SaleDto> Sales);
   
}
