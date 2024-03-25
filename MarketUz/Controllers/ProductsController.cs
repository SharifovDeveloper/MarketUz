using MarketUz.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Product;
using MarketUz.ResourceParameters;
using Microsoft.AspNetCore.Mvc;

namespace MarketUz.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;

        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetProductsAsync(
            [FromQuery] ProductResourceParameters productResourceParameters)
        {
            var products = _productService.GetProducts(productResourceParameters);

            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public ActionResult<ProductDto> Get(int id)
        {
            var product = _productService.GetProductById(id);

            if (product is null)
            {
                return NotFound($"Product with id: {id} does not exist.");
            }

            return Ok(product);
        }

        [HttpPost]
        public ActionResult Post([FromBody] ProductForCreateDto product)
        {
            _productService.CreateProduct(product);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ProductForUpdateDto product)
        {
            if (id != product.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {product.Id}.");
            }

            _productService.UpdateProduct(product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _productService.DeleteProduct(id);
        }
    }

    public class ProductParams
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
    }
}
