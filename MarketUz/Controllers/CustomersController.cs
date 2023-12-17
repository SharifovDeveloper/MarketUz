using DiyorMarket.Domain.DTOs.Category;
using DiyorMarket.Domain.DTOs.Customer;
using DiyorMarket.Domain.Interfaces.Services;
using DiyorMarket.Services;
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
            try
            {
                var customers = _customerService.GetCustomers();

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was error returning customers. {ex.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
        public ActionResult<CustomerDto> Get(int id)
        {
            try
            {
                var customer = _customerService.GetCustomerById(id);

                if (customer is null)
                {
                    return NotFound($"Customer with id: {id} does not exist.");
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was error getting customer with id: {id}. {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] CustomerForCreateDto customer)
        {
            try
            {
                _customerService.CreateCustomer(customer);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was an error creating new customer. {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CustomerForUpdateDto customer)
        {
            if (id != customer.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {customer.Id}.");
            }

            try
            {
                _customerService.UpdateCustomer(customer);

                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(500,
                    $"There was an error updating customer with id: {id}. {ex.Message}");

            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _customerService.DeleteCustomer(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was an error deleting customer with id: {id}. {ex.Message}");
            }
        }
    }
}
