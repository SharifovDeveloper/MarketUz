using ClosedXML.Excel;
using MarketUz.Domain.Interfaces.Services;
using MarketUz.Domain.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Inflow.Core.Sale;

namespace MarketUz.Controllers
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
        public ActionResult<IEnumerable<SaleDto>> GetSalesAsync(
            [FromQuery] SaleResourceParameters saleResourceParameters)
        {
            var sales = _saleService.GetSales(saleResourceParameters);

            return Ok(sales);
        }

        [HttpGet("export")]
        public ActionResult ExportSales()
        {
            var category = _saleService.GetAllSales();

            using XLWorkbook wb = new XLWorkbook();
            var sheet1 = wb.AddWorksheet(GetSalesDataTable(category), "Sales");

            sheet1.Column(1).Style.Font.FontColor = XLColor.Red;

            sheet1.Columns(2, 4).Style.Font.FontColor = XLColor.Blue;

            sheet1.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.Black;
            sheet1.Row(1).Style.Font.FontColor = XLColor.White;

            sheet1.Row(1).Style.Font.Bold = true;
            sheet1.Row(1).Style.Font.Shadow = true;
            sheet1.Row(1).Style.Font.Underline = XLFontUnderlineValues.Single;
            sheet1.Row(1).Style.Font.VerticalAlignment = XLFontVerticalTextAlignmentValues.Superscript;
            sheet1.Row(1).Style.Font.Italic = true;

            sheet1.Rows(2, 3).Style.Font.FontColor = XLColor.AshGrey;

            using MemoryStream ms = new MemoryStream();
            wb.SaveAs(ms);
            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sales.xlsx");
        }

        [HttpGet("CustomersSale/{customersId}")]
        public ActionResult<IEnumerable<SaleDto>> GetCustomersSale(int customersId)
        {
            var customersSales = _saleService.GetCustomersSale(customersId);
            return Ok(customersSales);
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


        [HttpPost]
        public ActionResult Post([FromBody] SaleForCreateDto sale)
        {
            var createSale = _saleService.CreateSale(sale);

            return CreatedAtAction(nameof(Get), new { createSale.Id }, createSale);
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

        private DataTable GetSalesDataTable(IEnumerable<SaleDto> saleDtos)
        {
            DataTable table = new DataTable();
            table.TableName = "Sales Data";
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("SaleDate", typeof(DateTime));
            table.Columns.Add("TotalDue", typeof(decimal));
            table.Columns.Add("CustomerId", typeof(int));

            foreach (var sale in saleDtos)
            {
                table.Rows.Add(sale.Id,
                    sale.SaleDate,
                    sale.TotalDue,
                    sale.CustomerId);
            }

            return table;
        }
    }
}