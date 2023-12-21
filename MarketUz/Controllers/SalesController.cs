using DiyorMarket.Domain.DTOs.Sale;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Sale;
using MarketUz.Domain.DTOs.SaleItem;
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
            var sales = _saleService.GetSales();
            return Ok(sales);
        }

        [HttpGet("{id}", Name = "GetSaleById")]
        public ActionResult<SaleDto> Get(int id)
        {            
            var sale = _saleService.GetSaleById(id);

            if (sale is null)
            {
                return NotFound($"Sale with id: {id} does not exist.");
            }
            return Ok(sale);
            
        }

        [HttpGet("{id}/saleItems")]
        public ActionResult<SaleItemDto> GetSaleItemsBySaleId(int id)
        {
           
            var saleItems = _saleItemService.GetSaleItems();

            var filteredSaleItems = saleItems.Where(x => x.SaleId == id).ToList();

            return Ok(filteredSaleItems);
           
        }

        [HttpPost]
        public ActionResult Post([FromBody] SaleForCreateDto sale)
        {         
           _saleService.CreateSale(sale);

            return StatusCode(201);           
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SaleForUpdateDto sale)
        {
            if (id != sale.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {sale.Id}.");
            }
          
            _saleService.UpdateSale(sale);

            return NoContent();           
           
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
           
            _saleService.DeleteSale(id);

            return NoContent();
           
        }
    }
}
