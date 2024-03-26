﻿using MarketUz.Domain.DTOs.Customer;
using MarketUz.Domain.ResourceParameters;
using MarketUz.Domain.Responses;

namespace MarketUz.Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDto> GetCustomers();
        GetBaseResponse<CustomerDto> GetCustomers(CustomerResourceParameters customerResourceParameters);
        CustomerDto? GetCustomerById(int id);
        CustomerDto CreateCustomer(CustomerForCreateDto customerToCreate);
        CustomerDto UpdateCustomer(CustomerForUpdateDto customerToUpdate);
        void DeleteCustomer(int id);
    }
}
