﻿using MarketUz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiyorMarket.Domain.DTOs.Sale
{
    public record SaleForCreateDto(
        DateTime saleDate,
        int CustomerId);
    
}
