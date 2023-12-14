using AutoMapper;
using MarketUz.Domain.DTOs.SaleItem;
using MarketUz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketUz.Domain.Mappings
{
    public class SaleItemMappings:Profile
    {
        public SaleItemMappings() 
        {
            CreateMap<SaleItem, SaleItemDto>();
            CreateMap<SaleItemDto, SaleItem>();
            CreateMap<SaleItemForCreateDto, SaleItem>();
            CreateMap<SaleItemForUpdateDto, SaleItem>();
        }
    }
}
