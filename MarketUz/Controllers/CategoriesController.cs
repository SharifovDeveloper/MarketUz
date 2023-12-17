using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Category;
using MarketUz.Domain.DTOs.Product;
using MarketUz.ResourceParameters;
using Microsoft.AspNetCore.Mvc;

namespace DiyorMarketApi.Controllers
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
        public ActionResult<IEnumerable<CategoryDto>> Get()
        {
            var categories = _categoryService.GetCategories();

            return Ok(categories);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public ActionResult<CategoryDto> Get(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            return Ok(category);
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
            _categoryService.CreateCategory(category);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CategoryForUpdateDto category)
        {
            if (id != category.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {category.Id}.");
            }

            _categoryService.UpdateCategory(category);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);

            return NoContent();
        }
    }
}
