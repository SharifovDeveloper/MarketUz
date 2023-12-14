using AutoMapper;
using MarketUz.Domain.DTOs.Category;
using MarketUz.Domain.DTOs.Customer;
using MarketUz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketUz.Domain.Mappings
{
    public class CustomerMappings:Profile
    {
        public CustomerMappings() 
        {
           
            CreateMap<Customer,CustomerDto>()
                .ForMember(x => x.FullName,r => r.MapFrom(x => x.FirstName + "" + x.LastName));

            CreateMap<CustomerDto, Customer>();
            CreateMap<CustomerForCreateDto, Customer>();
            CreateMap<CustomerForUpdateDto, Customer>();

        }
    }
}
