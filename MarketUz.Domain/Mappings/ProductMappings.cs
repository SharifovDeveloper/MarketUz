using AutoMapper;
using MarketUz.Domain.DTOs.Product;
using MarketUz.Domain.Entities;

namespace MarketUz.Domain.Mappings
{
    public class ProductMappings : Profile
    {
        public ProductMappings()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<ProductForCreateDto, Product>();
            CreateMap<ProductForUpdateDto, Product>();



        }
    }
}
