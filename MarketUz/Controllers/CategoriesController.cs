using ClosedXML.Excel;
using MarketUz.Domain.Interfaces.Services;
using MarketUz.Domain.ResourceParameters;
using MarketUz.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using System.Data;
using Inflow.Core.Category;
using Inflow.Core.Product;

namespace MarketUz.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CategoriesController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> GetCategories(
            [FromQuery] CategoryResourceParameters categoryResourceParameters)
        {
            var categories = _categoryService.GetCategories(categoryResourceParameters);

            return Ok(categories);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public ActionResult<CategoryDto> Get(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            return Ok(category);
        }

        [HttpGet("export/xls")]
        public ActionResult ExportCustomers()
        {
            var categories = _categoryService.GetAllCategories();
            byte[] data = GenerateExcle(categories);

            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Categories.xlsx");
        }

        [HttpGet("export/pdf")]
        public IActionResult CreatePDFDocument()
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();

            PdfGrid pdfGrid = new PdfGrid();

            var categories = _categoryService.GetAllCategories();
            List<object> data = ConvertCategoriesToData(categories);

            pdfGrid.DataSource = data;

            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

            pdfGrid.Draw(page, new PointF(10, 10));

            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;

            string contentType = "application/pdf";
            string fileName = "categories.pdf";

            return File(stream, contentType, fileName);
        }

        [HttpGet("{id}/products")]
        public ActionResult<ProductDto> GetProductsByCategoryId(
            int id,
            ProductResourceParameters productResourceParameters)
        {
            var products = _productService.GetProducts(productResourceParameters);

            return Ok(products);
        }

        [HttpPost]
        public ActionResult Post([FromBody] CategoryForCreateDto category)
        {
            var createdCategory = _categoryService.CreateCategory(category);

            return CreatedAtAction(nameof(Get), new { createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CategoryForUpdateDto category)
        {
            if (id != category.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {category.Id}.");
            }

            var updatedCategory = _categoryService.UpdateCategory(category);

            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);

            return NoContent();
        }
        private static byte[] GenerateExcle(IEnumerable<CategoryDto> categoryDtos)
        {
            using XLWorkbook wb = new();
            var sheet1 = wb.AddWorksheet(GetCategoriesDataTable(categoryDtos), "Categories");

            sheet1.Columns(1, 3).Style.Font.FontColor = XLColor.Black;
            sheet1.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.Black;
            sheet1.Row(1).Style.Font.FontColor = XLColor.White;

            sheet1.Column(1).Width = 5;
            sheet1.Columns(2, 3).Width = 12;

            sheet1.Row(1).Style.Font.FontSize = 15;
            sheet1.Row(1).Style.Font.Bold = true;
            sheet1.Row(1).Style.Font.Shadow = true;
            sheet1.Row(1).Style.Font.VerticalAlignment = XLFontVerticalTextAlignmentValues.Superscript;
            sheet1.Row(1).Style.Font.Italic = false;

            using MemoryStream ms = new();
            wb.SaveAs(ms);

            return ms.ToArray();
        }
        private List<object> ConvertCategoriesToData(IEnumerable<CategoryDto> categories)
        {
            List<object> data = new List<object>();

            foreach (var category in categories)
            {
                data.Add(new { ID = category.Id, category.Name, category.NumberOfProduct });
            }

            return data;
        }
        private static DataTable GetCategoriesDataTable(IEnumerable<CategoryDto> categories)
        {
            DataTable table = new DataTable();
            table.TableName = "Categories Data";
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Number of Products", typeof(int));

            foreach (var category in categories)
            {
                table.Rows.Add(category.Id, category.Name, category.NumberOfProduct);
            }

            return table;
        }
    }
}
