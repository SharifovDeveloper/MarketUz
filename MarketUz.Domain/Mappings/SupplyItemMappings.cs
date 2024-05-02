using AutoMapper;
using MarketUz.Domain.Entities;
using Inflow.Core.SupplyItem;

namespace MarketUz.Domain.Mappings
{
    public class SupplyItemMappings : Profile
    {
        public SupplyItemMappings()
        {
            CreateMap<SupplyItemDto, SupplyItem>()
                .PreserveReferences();
            CreateMap<SupplyItem, SupplyItemDto>()
                .PreserveReferences();
            CreateMap<SupplyItemForCreateDto, SupplyItem>();
            CreateMap<SupplyItemForUpdateDto, SupplyItem>();
        }
    }
}