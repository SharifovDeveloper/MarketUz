using AutoMapper;
using MarketUz.Domain.DTOs.Supply;
using MarketUz.Domain.Entities;

namespace MarketUz.Domain.Mappings
{
    public class SupplMappings : Profile
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
