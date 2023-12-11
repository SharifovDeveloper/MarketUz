using MarketUz.Domain.Entities;
using MarketUz.Domain.Interfaces.Services;
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
        private readonly IProductService _service;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _service = productService;
            _logger = logger;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            _logger.LogInformation("getting all products");
            return ProductsService.GetProducts();
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            _logger.LogInformation($"Getting product with id: {id}");
            var product = ProductsService.GetProduct(id);

            if (product is null)
            {
                _logger.LogError($"Product with id: {id} not found.");
                _logger.LogCritical("Database is down!!!");
                return NotFound($"Product with id: {id} does not exist.");
            }

            return product;
        }

        // POST api/<ProductsController>
        [HttpPost]
        public ActionResult Post(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                ProductsService.Create(product);

                return StatusCode(201, product);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product productToUpdate)
        {
            ProductsService.Update(productToUpdate);
        }

        [HttpPatch("{id}")]
        public ActionResult PartiallyUpdateProduct(
            int id,
            JsonPatchDocument<Product> jsonPatch)
        {
            var product = ProductsService.GetProduct(id);

            if (product is null)
            {
                return NotFound($"Product with id: {id} does not exist.");
            }

            var productToPatch = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
            };

            jsonPatch.ApplyTo(productToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!TryValidateModel(productToPatch))
            {
                return BadRequest();
            }

            product.Name = productToPatch.Name;
            product.Price = productToPatch.Price;
            product.CategoryId = productToPatch.CategoryId;

            return Ok(productToPatch);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ProductsService.Delete(id);
        }
    }
}
