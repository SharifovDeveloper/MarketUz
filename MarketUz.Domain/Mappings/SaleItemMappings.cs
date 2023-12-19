using AutoMapper;
using MarketUz.Domain.DTOs.SaleItem;
using MarketUz.Domain.Entities;

namespace MarketUz.Domain.Mappings
{
    public class SaleItemMappings : Profile
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
