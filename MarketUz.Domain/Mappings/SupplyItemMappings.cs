using AutoMapper;
using MarketUz.Domain.DTOs.SupplyItem;
using MarketUz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketUz.Domain.Mappings
{
    public class SupplyItemMappings:Profile
    {
        public SupplyItemMappings() 
        {
            CreateMap<SupplyItemDto, SupplyItem>();
            CreateMap<SupplyItem, SupplyItemDto>();
            CreateMap<SupplyItemForCreateDto, SupplyItem>();
            CreateMap<SupplyItemForUpdateDto, SupplyItem>();
        }
    }
}
