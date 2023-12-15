using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Category;
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
            try
            {
                var categories = _categoryService.GetCategories();

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was error returning categories. {ex.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public ActionResult<CategoryDto> Get(int id)
        {
            try
            {
                var category = _categoryService.GetCategoryById(id);

                if (category is null)
                {
                    return NotFound($"Category with id: {id} does not exist.");
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was error getting category with id: {id}. {ex.Message}");
            }
        }

        //[HttpGet("{id}/products")]
        //public ActionResult<ProductDto> GetProductsByCategoryId(int id)
        //{
        //    try
        //    {
        //        var products = _productService.GetProducts();

        //        var filteredProducts = products.Where(x => x.CategoryId == id).ToList();

        //        return Ok(filteredProducts);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500,
        //            $"There was an error returning products for category: {id}. {ex.Message}");
        //    }
        //}

        [HttpPost]
        public ActionResult Post([FromBody] CategoryForCreateDto category)
        {
            try
            {
                _categoryService.CreateCategory(category);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was an error creating new category. {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CategoryForUpdateDto category)
        {
            if (id != category.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {category.Id}.");
            }

            try
            {
                _categoryService.UpdateCategory(category);

                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(500,
                    $"There was an error updating category with id: {id}. {ex.Message}");

            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _categoryService.DeleteCategory(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was an error deleting category with id: {id}. {ex.Message}");
            }
        }
    }
}
