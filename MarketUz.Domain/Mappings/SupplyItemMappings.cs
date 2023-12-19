using AutoMapper;
using MarketUz.Domain.DTOs.SupplyItem;
using MarketUz.Domain.Entities;

namespace MarketUz.Domain.Mappings
{
    public class SupplyItemMappings : Profile
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
