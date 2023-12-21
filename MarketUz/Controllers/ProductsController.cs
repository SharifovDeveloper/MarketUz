using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Product;
using MarketUz.ResourceParameters;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DiyorMarketApi.Controllers
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

        // GET: api/<ProductsController>
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetProductsAsync(
            [FromQuery] ProductResourceParameters productResourceParameters)
        {
            var products = _productService.GetProducts(productResourceParameters);

            return Ok(products);
        }

        // GET api/<ProductsController>/5
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

        // POST api/<ProductsController>
        [HttpPost]
        public ActionResult Post([FromBody] ProductForCreateDto product)
        {
            _productService.CreateProduct(product);

            return StatusCode(201);
        }

        // PUT api/<ProductsController>/5
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



        // DELETE api/<ProductsController>/5
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
