using DiyorMarket.Domain.DTOs.Category;
using DiyorMarket.Domain.DTOs.Product;
using DiyorMarket.Domain.DTOs.Sale;
using DiyorMarket.Domain.DTOs.SaleItem;
using DiyorMarket.Domain.Interfaces.Services;
using DiyorMarket.Services;
using DiyorMarketApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiyorMarket.Controllers
{
    [Route("api/sales")]
    [ApiController]
    public class SalesController : Controller
    {
        private readonly ISaleService _saleService;
        private readonly ISaleItemService _saleItemService;
        public SalesController(ISaleService saleService, ISaleItemService saleItemService)
        {
            _saleService = saleService;
            _saleItemService = saleItemService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SaleDto>> Get()
        {
            try
            {
                var sales = _saleService.GetSales();

                return Ok(sales);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was error returning sales. {ex.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetSaleById")]
        public ActionResult<SaleDto> Get(int id)
        {
            try
            {
                var sale = _saleService.GetSaleById(id);

                if (sale is null)
                {
                    return NotFound($"Sale with id: {id} does not exist.");
                }

                return Ok(sale);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was error getting sale with id: {id}. {ex.Message}");
            }
        }

        [HttpGet("{id}/saleItems")]
        public ActionResult<SaleItemDto> GetSaleItemsBySaleId(int id)
        {
            try
            {
                var saleItems = _saleItemService.GetSaleItems();

                var filteredSaleItems = saleItems.Where(x => x.SaleId == id).ToList();

                return Ok(filteredSaleItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was an error returning saleItems for sale: {id}. {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] SaleForCreateDto sale)
        {
            try
            {
                _saleService.CreateSale(sale);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was an error creating new sale. {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SaleForUpdateDto sale)
        {
            if (id != sale.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {sale.Id}.");
            }

            try
            {
                _saleService.UpdateSale(sale);

                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(500,
                    $"There was an error updating sale with id: {id}. {ex.Message}");

            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _saleService.DeleteSale(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was an error deleting sale with id: {id}. {ex.Message}");
            }
        }
    }
}
