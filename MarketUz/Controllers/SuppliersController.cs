using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Supplier;
using Microsoft.AspNetCore.Mvc;

namespace DiyorMarket.Controllers
{
    [Route("api/suppliers")]
    [ApiController]
    public class SuppliersController : Controller
    {
        private readonly ISupplierService _supplierService;
        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SupplierDto>> Get()
        {
           var suppliers = _supplierService.GetSuppliers();

           return Ok(suppliers);
           
        }

        [HttpGet("{id}", Name = "GetSupplierById")]
        public ActionResult<SupplierDto> Get(int id)
        {
          
            var supplier = _supplierService.GetSupplierById(id);

            if (supplier is null)
            {
                return NotFound($"Supplier with id: {id} does not exist.");
            }

            return Ok(supplier);
          
        }

        [HttpPost]
        public ActionResult Post([FromBody] SupplierForCreateDto supplier)
        {
            
            _supplierService.CreateSupplier(supplier);

            return StatusCode(201);
           
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SupplierForUpdateDto supplier)
        {
            if (id != supplier.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {supplier.Id}.");
            }

            _supplierService.UpdateSupplier(supplier);

            return NoContent();
           
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
          
            _supplierService.DeleteSupplier(id);

            return NoContent();
            
        }
    }
}
