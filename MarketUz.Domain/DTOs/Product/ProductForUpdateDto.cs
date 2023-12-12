using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketUz.Domain.DTOs.Product
{
    public record ProductForUpdateDto(
      int Id,
      string Name,
      int CategoryId);
 
}
