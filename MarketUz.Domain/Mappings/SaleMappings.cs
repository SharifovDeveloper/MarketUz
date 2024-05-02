using AutoMapper;
using MarketUz.Domain.Entities;
using Inflow.Core.Sale;

namespace MarketUz.Domain.Mappings
{
    public class SaleMappings : Profile
    {
        public SaleMappings()
        {
            CreateMap<SaleDto, Sale>();
            CreateMap<Sale, SaleDto>()
                .ForCtorParam(nameof(SaleDto.TotalDue), x => x.MapFrom(s => s.SaleItems.Sum(q => q.Quantity * q.UnitPrice)));
            CreateMap<SaleForCreateDto, Sale>();
            CreateMap<SaleForUpdateDto, Sale>();
        }
    }
}
