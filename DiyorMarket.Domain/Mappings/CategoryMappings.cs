using AutoMapper;
using DiyorMarket.Domain.DTOs.Category;
using DiyorMarket.Domain.Entities;

namespace DiyorMarket.Domain.Mappings
{
    public class CategoryMappings : Profile
    {
        public CategoryMappings() 
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(x => x.NumberOfProducts, r => r.MapFrom(x => x.Products.Count));
            CreateMap<CategoryDto, Category>();
            CreateMap<CategoryForCreateDto, Category>();
            CreateMap<CategoryForUpdateDto, Category>();
        }
    }
}
