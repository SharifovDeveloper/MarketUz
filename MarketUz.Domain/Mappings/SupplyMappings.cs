﻿using AutoMapper;
using MarketUz.Domain.Entities;
using Inflow.Core.Supply;

namespace MarketUz.Domain.Mappings
{
    public class SupplyMappings : Profile
    {
        public SupplyMappings()
        {
            CreateMap<SupplyDto, Supply>()
                .PreserveReferences();
            CreateMap<Supply, SupplyDto>()
                .ForCtorParam(nameof(SupplyDto.TotalDue), x => x.MapFrom(s => s.SupplyItems.Sum(q => q.Quantity * (decimal)q.UnitPrice)));
            CreateMap<SupplyForCreateDto, Supply>();
            CreateMap<SupplyForUpdateDto, Supply>();
        }
    }
}
