using AutoMapper;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Category;
using MarketUz.Domain.DTOs.Product;
using MarketUz.Domain.Entities;
using MarketUzApi.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketUzApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;

        public ProductsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<ProductDto>> Get()
        {
            try
            {
                var products = ProductsService.GetProducts();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was error returning products. {ex.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public ActionResult<ProductDto> Get(int id)
        {
            try
            {
                var product = ProductsService.GetProduct(id);

                if (product is null)
                {
                    return NotFound($"Product with id: {id} does not exist.");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was error getting product with id: {id}. {ex.Message}");
            }
        }



        [HttpPost]
        public ActionResult Post([FromBody] ProductForCreateDto product)
        {
            try
            {
                var productEntity = _mapper.Map<Product>(product);

                ProductsService.Create(productEntity);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was an error creating new product. {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ProductForUpdateDto product)
        {
            if (id != product.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {product.Id}.");
            }

            try
            {
                var productEntity = _mapper.Map<Product>(product);
                ProductsService.Update(productEntity);

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
                ProductsService.Delete(id);

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
