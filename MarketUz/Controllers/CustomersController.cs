using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Customer;
using Microsoft.AspNetCore.Mvc;

namespace DiyorMarket.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerDto>> Get()
        {
             
            var customers = _customerService.GetCustomers();

            return Ok(customers);
            
          
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
        public ActionResult<CustomerDto> Get(int id)
        {
           
            var customer = _customerService.GetCustomerById(id);

            if (customer is null)
            {
                return NotFound($"Customer with id: {id} does not exist.");
            }

            return Ok(customer);
            
        }

        [HttpPost]
        public ActionResult Post([FromBody] CustomerForCreateDto customer)
        {
          
            _customerService.CreateCustomer(customer);

            return StatusCode(201);
           
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CustomerForUpdateDto customer)
        {
            if (id != customer.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {customer.Id}.");
            }

            _customerService.UpdateCustomer(customer);

             return NoContent();
            
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
          
            _customerService.DeleteCustomer(id);

            return NoContent();
           
        }
    }
}
