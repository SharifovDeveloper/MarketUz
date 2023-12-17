using DiyorMarket.Domain.DTOs.Sale;
using DiyorMarket.Domain.DTOs.SaleItem;
using DiyorMarket.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiyorMarket.Controllers
{
    [Route("api/saleItems")]
    [ApiController]
    public class SaleItemsController : Controller
    {
        private readonly ISaleItemService _saleItemService;
        public SaleItemsController(ISaleItemService saleItemService)
        {
            _saleItemService = saleItemService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SaleItemDto>> Get()
        {
            try
            {
                var saleItems = _saleItemService.GetSaleItems();

                return Ok(saleItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was error returning saleItems. {ex.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetSaleItemById")]
        public ActionResult<SaleItemDto> Get(int id)
        {
            try
            {
                var saleItem = _saleItemService.GetSaleItemById(id);

                if (saleItem is null)
                {
                    return NotFound($"SaleItem with id: {id} does not exist.");
                }

                return Ok(saleItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was error getting saleItem with id: {id}. {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] SaleItemForCreateDto saleItem)
        {
            try
            {
                _saleItemService.CreateSaleItem(saleItem);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was an error creating new saleItem. {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SaleItemForUpdateDto saleItem)
        {
            if (id != saleItem.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {saleItem.Id}.");
            }

            try
            {
                _saleItemService.UpdateSaleItem(saleItem);

                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(500,
                    $"There was an error updating saleItem with id: {id}. {ex.Message}");

            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _saleItemService.DeleteSaleItem(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was an error deleting saleItem with id: {id}. {ex.Message}");
            }
        }
    }
}
