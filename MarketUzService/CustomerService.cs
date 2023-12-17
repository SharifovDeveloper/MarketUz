using AutoMapper;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Customer;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Exceptions;
using MarketUz.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

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

            var customers = _context.Customers.ToList();

            var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);

            return customerDtos;
        }

        public CustomerDto? GetCustomerById(int id)
        {

            var customer = _context.Customers.FirstOrDefault(x => x.Id == id);
            if (customer is null)
            {
                throw new EntityNotFoundException($"Customer with id: {id} not found");
            }

            var customerDto = _mapper.Map<CustomerDto>(customer);

            return customerDto;

        }

        public CustomerDto CreateCustomer(CustomerForCreateDto customerToCreate)
        {

            var customerEntity = _mapper.Map<Customer>(customerToCreate);

            _context.Customers.Add(customerEntity);
            _context.SaveChanges();

            var customerDto = _mapper.Map<CustomerDto>(customerEntity);

            return customerDto;

        }

        public void UpdateCustomer(CustomerForUpdateDto customerToUpdate)
        {

            var customerEntity = _mapper.Map<Customer>(customerToUpdate);

            _context.Customers.Update(customerEntity);
            _context.SaveChanges();

        }

        public void DeleteCustomer(int id)
        {

            var customer = _context.Customers.FirstOrDefault(x => x.Id == id);
            if (customer is not null)
            {
                _context.Customers.Remove(customer);
            }
            _context.SaveChanges();
        }

    }
}
