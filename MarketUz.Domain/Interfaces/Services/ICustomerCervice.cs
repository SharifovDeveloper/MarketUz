using MarketUz.Domain.DTOs.Category;
using MarketUz.Domain.DTOs.Customer;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDto> GetCustomers();
        CustomerDto GetCustomerById(int customerId);
        CategoryDto CreateCustomer(CustomerForCreateDto custumer);
        void UpdateCustomer(CustomerForUpdateDto custumer);
        void DeleteCustomer(int customerId);
    }
}
