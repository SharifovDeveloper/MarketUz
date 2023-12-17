using AutoMapper;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Customer;
using MarketUz.Domain.Entities;
using MarketUz.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace MarketUz.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly MarketUzDbContext _context;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IMapper mapper, MarketUzDbContext context, ILogger<CustomerService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<CustomerDto> GetCustomers()
        {
            try
            {
                var customers = _context.Customers.ToList();

                var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);

                return customerDtos;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Customer and CustomerDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database error occured while fetching customers.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while fetching customers.", ex.Message);
                throw;
            }
        }

        public CustomerDto? GetCustomerById(int id)
        {
            try
            {
                var customer = _context.Customers.FirstOrDefault(x => x.Id == id);

                var customerDto = _mapper.Map<CustomerDto>(customer);

                return customerDto;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Customer and CustomerDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError($"Database error occured while fetching customer with id: {id}.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while fetching customer with id: {id}.", ex.Message);
                throw;
            }
        }

        public CustomerDto CreateCustomer(CustomerForCreateDto customerToCreate)
        {
            try
            {
                var customerEntity = _mapper.Map<Customer>(customerToCreate);

                _context.Customers.Add(customerEntity);
                _context.SaveChanges();

                var customerDto = _mapper.Map<CustomerDto>(customerEntity);

                return customerDto;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Customer and CustomerDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database error occured while creating new customer.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while creating new customer.", ex.Message);
                throw;
            }
        }

        public void UpdateCustomer(CustomerForUpdateDto customerToUpdate)
        {
            try
            {
                var customerEntity = _mapper.Map<Customer>(customerToUpdate);

                _context.Customers.Update(customerEntity);
                _context.SaveChanges();
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Customer and CustomerDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database error occured while updating customer.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while updating customer.", ex.Message);
                throw;
            }
        }

        public void DeleteCustomer(int id)
        {
            try
            {
                var customer = _context.Customers.FirstOrDefault(x => x.Id == id);
                if (customer is not null)
                {
                    _context.Customers.Remove(customer);
                }
                _context.SaveChanges();
            }
            catch (DbException ex)
            {
                _logger.LogError($"Database error occured while deleting customer with id: {id}.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while deleting customer with id: {id}.", ex.Message);
                throw;
            }
        }
    }
}
