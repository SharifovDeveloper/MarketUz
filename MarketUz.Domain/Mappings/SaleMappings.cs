using AutoMapper;
using DiyorMarket.Domain.DTOs.Sale;
using MarketUz.Domain.DTOs.Sale;
using MarketUz.Domain.Entities;

namespace MarketUz.Domain.Mappings
{
    public class SaleMappings : Profile
    {
        public SaleMappings()
        {
            CreateMap<Sale, SaleDto>();
            CreateMap<SaleDto, Sale>();
            CreateMap<SaleForCreateDto, Sale>();
            CreateMap<SaleForUpdateDto, Sale>();
        }
    }
}
