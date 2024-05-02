using AutoMapper;
using MarketUz.Domain.Entities;
using Inflow.Core.Category;

namespace MarketUz.Domain.Mappings
{
    public class CategoryMappings : Profile
    {
        public CategoryMappings()
        {
            CreateMap<Category, CategoryDto>()
                  .ForMember(x => x.NumberOfProduct, r => r.MapFrom(x => x.Products.Count()));
            CreateMap<CategoryDto, Category>();
            CreateMap<CategoryForCreateDto, Category>();
            CreateMap<Category, CategoryForCreateDto>();
            CreateMap<CategoryForUpdateDto, Category>();
            CreateMap<Category, CategoryForUpdateDto>();
        }
    }
}
