//using AutoMapper;
//using MarketUz.Domain.DTOs.Category;
//using MarketUz.Domain.DTOs.Customer;
//using MarketUz.Domain.DTOs.Product;
//using MarketUz.Domain.Entities;
//using MarketUzApi.Services;
//using Microsoft.AspNetCore.Mvc;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace MarketUz.Controllers
//{
//    [Route("api/[customers]")]
//    [ApiController]
//    public class CustomersController : ControllerBase
//    {
//        private readonly IMapper _mapper;

//        public CustomersController(IMapper mapper)
//        {
//            _mapper = mapper;
//        }

//        [HttpGet]
//        [HttpHead]
//        public ActionResult<IEnumerable<CustomerDto>> Get()
//        {
//            try
//            {
//                var categories = CustomersController.GetCategories();

//                return Ok(categories);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500,
//                    $"There was error returning categories. {ex.Message}");
//            }
//        }

//        [HttpGet("{id}", Name = "GetCategoryById")]
//        public ActionResult<CategoryDto> Get(int id)
//        {
//            try
//            {
//                var category = CustomersController(id);

//                if (category is null)
//                {
//                    return NotFound($"Category with id: {id} does not exist.");
//                }

//                return Ok(category);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500,
//                    $"There was error getting category with id: {id}. {ex.Message}");
//            }
//        }

      

//        [HttpPost]
//        public ActionResult Post([FromBody] CategoryForCreateDto category)
//        {
//            try
//            {
//                var categoryEntity = _mapper.Map<Category>(category);

//                CategoriesService.Create(categoryEntity);

//                return StatusCode(201);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500,
//                    $"There was an error creating new category. {ex.Message}");
//            }
//        }

//        [HttpPut("{id}")]
//        public ActionResult Put(int id, [FromBody] CategoryForUpdateDto category)
//        {
//            if (id != category.Id)
//            {
//                return BadRequest(
//                    $"Route id: {id} does not match with parameter id: {category.Id}.");
//            }

//            try
//            {
//                var categoryEntity = _mapper.Map<Category>(category);
//                CategoriesService.Update(categoryEntity);

//                return NoContent();
//            }
//            catch (Exception ex)
//            {

//                return StatusCode(500,
//                    $"There was an error updating category with id: {id}. {ex.Message}");

//            }
//        }

//        [HttpDelete("{id}")]
//        public ActionResult Delete(int id)
//        {
//            try
//            {
//                CategoriesService.Delete(id);

//                return NoContent();
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500,
//                    $"There was an error deleting category with id: {id}. {ex.Message}");
//            }
//        }
//    }
//}

