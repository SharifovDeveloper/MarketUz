using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetProductsAsync(
            [FromQuery] ProductResourceParameters productResourceParameters)
        {
            try
            {
                var products = _productService.GetProducts(productResourceParameters);

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was error returning products. {ex.Message}");
            }
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}", Name = "GetProductById")]
        public ActionResult<ProductDto> Get(int id)
        {
            throw new Exception();
            try
            {
                var product = _productService.GetProductById(id);

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

        // POST api/<ProductsController>
        [HttpPost]
        public ActionResult Post([FromBody] ProductForCreateDto product)
        {
            try
            {
                _productService.CreateProduct(product);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"There was an error creating new product. {ex.Message}");
            }
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

            try
            {
                _productService.UpdateProduct(product);

                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(500,
                    $"There was an error updating product with id: {id}. {ex.Message}");

            }
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
