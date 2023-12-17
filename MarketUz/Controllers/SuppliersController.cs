using DiyorMarket.Domain.DTOs.Sale;
using DiyorMarket.Domain.DTOs.Supplier;
using DiyorMarket.Domain.Interfaces.Services;
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
            try
            {
                var suppliers = _supplierService.GetSuppliers();

                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was error returning suppliers. {ex.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetSupplierById")]
        public ActionResult<SupplierDto> Get(int id)
        {
            try
            {
                var supplier = _supplierService.GetSupplierById(id);

                if (supplier is null)
                {
                    return NotFound($"Supplier with id: {id} does not exist.");
                }

                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was error getting supplier with id: {id}. {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] SupplierForCreateDto supplier)
        {
            try
            {
                _supplierService.CreateSupplier(supplier);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was an error creating new supplier. {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SupplierForUpdateDto supplier)
        {
            if (id != supplier.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {supplier.Id}.");
            }

            try
            {
                _supplierService.UpdateSupplier(supplier);

                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(500,
                    $"There was an error updating supplier with id: {id}. {ex.Message}");

            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _supplierService.DeleteSupplier(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was an error deleting supplier with id: {id}. {ex.Message}");
            }
        }
    }
}
