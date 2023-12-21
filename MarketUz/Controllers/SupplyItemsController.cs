using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.SupplyItem;
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
           
            var supplyItems = _supplyItemService.GetSupplyItems();

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
           
            _supplyItemService.CreateSupplyItem(supplyItem);

            return StatusCode(201);
           
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
