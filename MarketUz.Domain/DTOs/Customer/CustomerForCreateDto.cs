using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketUz.Domain.DTOs.Customer
{
    public record CustomerForCreateDto(
        string FirstName,
        string LastName,
        string PhoneNumber);
   
}
