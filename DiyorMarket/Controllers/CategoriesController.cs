using AutoMapper;
using MarketUz.Domain.DTOs.Category;
using MarketUz.Domain.DTOs.Product;
using MarketUz.Domain.Entities;
using MarketUzApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketUzApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;

        public CategoriesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> Get()
        {
            try
            {
                var categories = CategoriesService.GetCategories();

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
                var category = CategoriesService.GetCategory(id);

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

        [HttpGet("{id}/products")]
        public ActionResult<ProductDto> GetProductsByCategoryId(int id)
        {
            try
            {
                var products = ProductsService.GetProducts();

                var filteredProducts = products.Where(x => x.CategoryId == id).ToList();

                return Ok(filteredProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was an error returning products for category: {id}. {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] CategoryForCreateDto category)
        {
            try
            {
                var categoryEntity = _mapper.Map<Category>(category);

                CategoriesService.Create(categoryEntity);

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
                var categoryEntity = _mapper.Map<Category>(category);
                CategoriesService.Update(categoryEntity);

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
                CategoriesService.Delete(id);

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
