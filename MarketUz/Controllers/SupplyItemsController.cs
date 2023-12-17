using DiyorMarket.Domain.DTOs.Supply;
using DiyorMarket.Domain.DTOs.SupplyItem;
using DiyorMarket.Domain.Interfaces.Services;
using DiyorMarket.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiyorMarket.Controllers
{
    [Route("api/supplyItems")]
    [ApiController]
    public class SupplyItemsController : Controller
    {
        private readonly ISupplyItemService _supplyItemService;
        public SupplyItemsController(ISupplyItemService supplyItemService)
        {
            _supplyItemService = supplyItemService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SupplyItemDto>> Get()
        {
            try
            {
                var supplyItems = _supplyItemService.GetSupplyItems();

                return Ok(supplyItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was error returning supplyItems. {ex.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetSupplyItemById")]
        public ActionResult<SupplyItemDto> Get(int id)
        {
            try
            {
                var supplyItem = _supplyItemService.GetSupplyItemById(id);

                if (supplyItem is null)
                {
                    return NotFound($"SupplyItem with id: {id} does not exist.");
                }

                return Ok(supplyItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was error getting supplyItem with id: {id}. {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] SupplyItemForCreateDto supplyItem)
        {
            try
            {
                _supplyItemService.CreateSupplyItem(supplyItem);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was an error creating new supplyItem. {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SupplyItemForUpdateDto supplyItem)
        {
            if (id != supplyItem.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {supplyItem.Id}.");
            }

            try
            {
                _supplyItemService.UpdateSupplyItem(supplyItem);

                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(500,
                    $"There was an error updating supplyItem with id: {id}. {ex.Message}");

            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _supplyItemService.DeleteSupplyItem(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was an error deleting supplyItem with id: {id}. {ex.Message}");
            }
        }
    }
}
