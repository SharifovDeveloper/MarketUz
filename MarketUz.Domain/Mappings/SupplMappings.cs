using AutoMapper;
using MarketUz.Domain.DTOs.Supplier;
using MarketUz.Domain.DTOs.Supply;
using MarketUz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketUz.Domain.Mappings
{
    public class SupplMappings:Profile
    {
        public SupplMappings() 
        {
            CreateMap<Supply, SupplyDto>();
            CreateMap<SupplyDto, Supply>();
            CreateMap<SupplyForCreateDto, Supply>();
            CreateMap<SupplyForUpdateDto, Supply>();
        }
    }
}
