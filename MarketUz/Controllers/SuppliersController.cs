using ClosedXML.Excel;
using MarketUz.Domain.DTOs.Supplier;
using MarketUz.Domain.Interfaces.Services;
using MarketUz.Domain.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using System.Data;

namespace MarketUz.Controllers
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
        public ActionResult<IEnumerable<SupplierDto>> GetSuppliersAsync(
            [FromQuery] SupplierResourceParameters supplierResourceParameters)
        {
            var suppliers = _supplierService.GetSuppliers(supplierResourceParameters);

            return Ok(suppliers);
        }

        [HttpGet("export/xls")]
        public ActionResult ExportSuppliers()
        {
            var suppliers = _supplierService.GetAllSuppliers();
            byte[] data = GenerateExcle(suppliers);

            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Suppliers.xlsx");
        }
        [HttpGet("export/pdf")]
        public IActionResult CreatePDFDocument()
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();

            PdfGrid pdfGrid = new PdfGrid();

            var suppliers = _supplierService.GetAllSuppliers();

            List<object> data = ConvertSuppliersToData(suppliers);

            pdfGrid.DataSource = data;

            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

            pdfGrid.Draw(page, new PointF(10, 10));

            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;

            string contentType = "application/pdf";
            string fileName = "suppliers.pdf";

            return File(stream, contentType, fileName);
        }

        [HttpGet("{id}", Name = "GetSupplierById")]
        public ActionResult<SupplierDto> Get(int id)
        {
            var supplier = _supplierService.GetSupplierById(id);

            if (supplier is null)
            {
                return NotFound($"Supplier with id: {id} does not exist.");
            }

            return Ok(supplier);
        }

        [HttpPost]
        public ActionResult Post([FromBody] SupplierForCreateDto supplier)
        {
            var createSupplier = _supplierService.CreateSupplier(supplier);

            return CreatedAtAction(nameof(Get), new { createSupplier.Id }, createSupplier);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SupplierForUpdateDto supplier)
        {
            if (id != supplier.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {supplier.Id}.");
            }

            var updatedSupplier = _supplierService.UpdateSupplier(supplier);

            return Ok(updatedSupplier);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _supplierService.DeleteSupplier(id);

            return NoContent();
        }
        private List<object> ConvertSuppliersToData(IEnumerable<SupplierDto> supplierDtos)
        {
            List<object> data = new List<object>();

            foreach (var supplier in supplierDtos)
            {
                data.Add(new { ID = supplier.Id, supplier.FirstName, supplier.LastName, supplier.Company, supplier.PhoneNumber });
            }

            return data;
        }
        private static byte[] GenerateExcle(IEnumerable<SupplierDto> supplierDtos)
        {
            using XLWorkbook wb = new();
            var sheet1 = wb.AddWorksheet(GetSuppliersTable(supplierDtos), "Suppliers");

            sheet1.Columns(1, 3).Style.Font.FontColor = XLColor.Black;
            sheet1.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.Black;
            sheet1.Row(1).Style.Font.FontColor = XLColor.White;

            sheet1.Column(1).Width = 5;
            sheet1.Columns(2, 3).Width = 12;
            sheet1.Column(4).Width = 20;
            sheet1.Column(5).Width = 28;

            sheet1.Row(1).Style.Font.FontSize = 15;
            sheet1.Row(1).Style.Font.Bold = true;
            sheet1.Row(1).Style.Font.Shadow = true;
            sheet1.Row(1).Style.Font.VerticalAlignment = XLFontVerticalTextAlignmentValues.Superscript;
            sheet1.Row(1).Style.Font.Italic = false;

            using MemoryStream ms = new();
            wb.SaveAs(ms);

            return ms.ToArray();
        }
        private static DataTable GetSuppliersTable(IEnumerable<SupplierDto> supplierDtos)
        {
            DataTable table = new DataTable();
            table.TableName = "Suppliers Data";
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("First Name", typeof(string));
            table.Columns.Add("Last Name", typeof(string));
            table.Columns.Add("Phone Number", typeof(string));
            table.Columns.Add("Company", typeof(string));

            foreach (var supplier in supplierDtos)
            {
                table.Rows.Add(supplier.Id,
                    supplier.FirstName,
                    supplier.LastName,
                    supplier.PhoneNumber,
                    supplier.Company);
            }

            return table;
        }
    }
}
