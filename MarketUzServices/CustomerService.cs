using AutoMapper;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Category;
using MarketUz.Domain.DTOs.Customer;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiyorMarket.Services;

public class CustomerService : ICustomerService
{
    private readonly IMapper _mapper;
    private readonly ICommonRepository _repository;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(IMapper mapper, ICommonRepository repository, ILogger<CustomerService> logger)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public IEnumerable<CustomerDto> GetCustomers()
    {
        try
        {
            var Customers = _repository.Customer.FindAll();
            var customerDto = _mapper.Map<IEnumerable<CustomerDto>>(Customers);

            return customerDto;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError("Database error fetching customer ", ex);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error fetching customer ", ex);
            throw;
        }
    }
    public CustomerDto GetCustomerById(int id)
    {
        try
        {
            var customer = _repository.Customer.FindById(id);
            var customerDto = _mapper.Map<CustomerDto>(customer);

            return customerDto;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError($"Database error fetching customer with id : {id}", ex);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error fetching customer with id : {id}", ex);
            throw;
        }
    }
    public CustomerDto CreateCustomer(CustomerForCreateDto customer)
    {
        try
        {
            var customerEntity = _mapper.Map<Customer>(customer);
            var createdEntity = _repository.Customer.Create(customerEntity);

            _repository.SaveChanges();

            return _mapper.Map<CustomerDto>(customerEntity);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError("Database error creating new customer ", ex);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error creating new customer ", ex);
            throw;
        }
    }
    public void UpdateCustomer(CustomerForUpdateDto customer)
    {
        try
        {
            var customerEntity = _mapper.Map<Customer>(customer);
            _repository.Customer.Update(customerEntity);

            _repository.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError($"Database error updating customer with id : {customer.Id}", ex);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error updating customer with id : {customer.Id}", ex);
            throw;
        }
    }
    public void DeleteCustomer(int id)
    {
        try
        {
            _repository.Customer.Delete(id);
            _repository.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError($"Database error deleting category with id : {id}", ex);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error deleting category with id : {id}", ex);
            throw;
        }
    }

    CategoryDto ICustomerService.CreateCustomer(CustomerForCreateDto custumer)
    {
        throw new NotImplementedException();
    }
}
