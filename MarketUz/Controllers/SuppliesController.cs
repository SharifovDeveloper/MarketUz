using ClosedXML.Excel;
using MarketUz.Domain.Interfaces.Services;
using MarketUz.Domain.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Inflow.Core.Supply;

namespace MarketUz.Controllers
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
        public ActionResult<IEnumerable<SupplyDto>> GetSuppliesAsync(
            [FromQuery] SupplyResourceParameters supplyResourceParameters)
        {
            var supplies = _supplyService.GetSupplies(supplyResourceParameters);

            return Ok(supplies);
        }

        [HttpGet("export")]
        public ActionResult ExportSupplies()
        {
            var category = _supplyService.GetAllSupplies();

            using XLWorkbook wb = new XLWorkbook();
            var sheet1 = wb.AddWorksheet(GetSuppliesDataTable(category), "Supplies");

            sheet1.Column(1).Style.Font.FontColor = XLColor.Red;

            sheet1.Columns(2, 4).Style.Font.FontColor = XLColor.Blue;

            sheet1.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.Black;
            //sheet1.Row(1).Cells(1,3).Style.Fill.BackgroundColor = XLColor.Yellow;
            sheet1.Row(1).Style.Font.FontColor = XLColor.White;

            sheet1.Row(1).Style.Font.Bold = true;
            sheet1.Row(1).Style.Font.Shadow = true;
            sheet1.Row(1).Style.Font.Underline = XLFontUnderlineValues.Single;
            sheet1.Row(1).Style.Font.VerticalAlignment = XLFontVerticalTextAlignmentValues.Superscript;
            sheet1.Row(1).Style.Font.Italic = true;

            sheet1.Rows(2, 3).Style.Font.FontColor = XLColor.AshGrey;

            using MemoryStream ms = new MemoryStream();
            wb.SaveAs(ms);
            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Supplies.xlsx");
        }

        [HttpGet("{id}", Name = "GetSupplyById")]
        public ActionResult<SupplyDto> Get(int id)
        {
            var supply = _supplyService.GetSupplyById(id);

            if (supply is null)
            {
                return NotFound($"Supply with id: {id} does not exist.");
            }

            return Ok(supply);
        }

        [HttpPost]
        public ActionResult Post([FromBody] SupplyForCreateDto supply)
        {
            var createSupply = _supplyService.CreateSupply(supply);

            return CreatedAtAction(nameof(Get), new { createSupply.Id }, createSupply);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SupplyForUpdateDto supply)
        {
            if (id != supply.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {supply.Id}.");
            }

            _supplyService.UpdateSupply(supply);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _supplyService.DeleteSupply(id);

            return NoContent();
        }

        private DataTable GetSuppliesDataTable(IEnumerable<SupplyDto> supplyDtos)
        {
            DataTable table = new DataTable();
            table.TableName = "Supplies Data";
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("TotalDue", typeof(decimal));
            table.Columns.Add("SupplyDate", typeof(DateTime));
            table.Columns.Add("SupplierId", typeof(int));

            foreach (var supply in supplyDtos)
            {
                table.Rows.Add(supply.Id,
                    supply.TotalDue,
                    supply.SupplyDate,
                    supply.SupplierId);
            }

            return table;
        }
    }
}
