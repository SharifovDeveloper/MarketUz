using MarketUz.Domain.Interfaces.Services;
using MarketUz.Domain.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using Inflow.Core.SupplyItem;

namespace MarketUz.Controllers
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
        public ActionResult<IEnumerable<SupplyItemDto>> Get(
            [FromQuery] SupplyItemResourceParameters supplyItemResourceParameters)
        {
            var supplyItems = _supplyItemService.GetSupplyItems(supplyItemResourceParameters);

            return Ok(supplyItems);
        }

        [HttpGet("{id}", Name = "GetSupplyItemById")]
        public ActionResult<SupplyItemDto> Get(int id)
        {
            var supplyItem = _supplyItemService.GetSupplyItemById(id);

            if (supplyItem is null)
            {
                return NotFound($"SupplyItem with id: {id} does not exist.");
            }

            return Ok(supplyItem);
        }

        [HttpPost]
        public ActionResult Post([FromBody] SupplyItemForCreateDto supplyItem)
        {
            var createSupplyItem = _supplyItemService.CreateSupplyItem(supplyItem);

            return CreatedAtAction(nameof(Get), new { createSupplyItem.Id }, createSupplyItem);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SupplyItemForUpdateDto supplyItem)
        {
            if (id != supplyItem.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {supplyItem.Id}.");
            }

            _supplyItemService.UpdateSupplyItem(supplyItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _supplyItemService.DeleteSupplyItem(id);

            return NoContent();
        }
    }
}
