using AutoMapper;
using DiyorMarket.Domain.DTOs.Sale;
using MarketUz.Domain.DTOs.Sale;
using MarketUz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketUz.Domain.Mappings
{
    public class SaleMappings:Profile
    {
        public SaleMappings() 
        { 
           CreateMap<Sale,SaleDto>();
           CreateMap<SaleDto,Sale>();
           CreateMap<SaleForCreateDto, Sale>();
           CreateMap<SaleForUpdateDto, Sale>();
        }
    }
}
