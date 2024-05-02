using AutoMapper;
using MarketUz.Domain.Entities;
using Inflow.Core.Supplier;

namespace MarketUz.Domain.Mappings
{
    public class SupplierMappings : Profile
    {
        public SupplierMappings()
        {
            CreateMap<SupplierDto, Supplier>()
                .PreserveReferences();
            CreateMap<Supplier, SupplierDto>()
                .PreserveReferences();
            CreateMap<SupplierForCreateDto, Supplier>();
            CreateMap<SupplierForUpdateDto, Supplier>();
        }
    }
}

