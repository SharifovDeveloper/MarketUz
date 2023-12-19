using AutoMapper;
using MarketUz.Domain.DTOs.Supplier;
using MarketUz.Domain.Entities;

namespace MarketUz.Domain.Mappings
{
    public class SupplierMappings : Profile
    {
        public SupplierMappings()
        {
            CreateMap<Supplier, SupplierDto>()
                .ForMember(x => x.FullName, r => r.MapFrom(x => x.FirstName + "" + x.LastName));
            CreateMap<SupplierDto, Supplier>();
            CreateMap<SupplierForCreateDto, Supplier>();
            CreateMap<SupplierForUpdateDto, Supplier>();
        }
    }
}
