using DiyorMarket.Domain.DTOs.Supplier;
using DiyorMarket.Domain.DTOs.Supply;
using DiyorMarket.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiyorMarket.Controllers
{
    [Route("api/supplies")]
    [ApiController]
    public class SuppliesController : Controller
    {      
        private readonly ISupplyService _supplyService;
        public SuppliesController(ISupplyService supplyService)
        {
            _supplyService = supplyService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SupplyDto>> Get()
        {
            try
            {
                var supplies = _supplyService.GetSupplies();

                return Ok(supplies);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was error returning supplies. {ex.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetSupplyById")]
        public ActionResult<SupplyDto> Get(int id)
        {
            try
            {
                var supply = _supplyService.GetSupplyById(id);

                if (supply is null)
                {
                    return NotFound($"Supply with id: {id} does not exist.");
                }

                return Ok(supply);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was error getting supply with id: {id}. {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] SupplyForCreateDto supply)
        {
            try
            {
                _supplyService.CreateSupply(supply);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was an error creating new supply. {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SupplyForUpdateDto supply)
        {
            if (id != supply.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {supply.Id}.");
            }

            try
            {
                _supplyService.UpdateSupply(supply);

                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(500,
                    $"There was an error updating supply with id: {id}. {ex.Message}");

            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _supplyService.DeleteSupply(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was an error deleting supply with id: {id}. {ex.Message}");
            }
        }
    }
}
