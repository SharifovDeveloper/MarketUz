

using DiyorMarket.Domain.DTOs.Category;
using DiyorMarket.Domain.DTOs.Customer;
using DiyorMarket.Domain.Entities;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface ICustomerCervice
    {
        IEnumerable<CustomerDto> GetCustomers();
        CustomerDto GetCustomerById(int customerId);
        CategoryDto CreateCustomer(CustomerForCreateDto custumer);
        void UpdateCustomer(CustomerForUpdateDto custumer);
        void DeleteCustomer(int customerId);
    }
}
