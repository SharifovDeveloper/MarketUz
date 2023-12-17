using AutoMapper;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Customer;
using MarketUz.Domain.DTOs.Product;
using MarketUz.Domain.Entities;
using MarketUz.Infrastructure.Persistence;
using MarketUz.ResourceParameters;
using Microsoft.Extensions.Logging;
using System.Data.Common;
namespace MarketUz.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly MarketUzDbContext _context;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IMapper mapper, MarketUzDbContext context, ILogger<ProductService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<ProductDto> GetProducts(ProductResourceParameters productResourceParameters)
        {
            try
            {
                var query = _context.Products.AsQueryable();

                if (productResourceParameters.CategoryId is not null)
                {
                    query = query.Where(x => x.CategoryId == productResourceParameters.CategoryId);
                }

                if (!string.IsNullOrWhiteSpace(productResourceParameters.SearchString))
                {
                    query = query.Where(x => x.Name.Contains(productResourceParameters.SearchString) 
                    || x.Description.Contains(productResourceParameters.SearchString));
                }

                if (productResourceParameters.Price is not null)
                {
                    query = query.Where(x => x.Price == productResourceParameters.Price);
                }

                if (productResourceParameters.PriceLessThan is not null)
                {
                    query = query.Where(x => x.Price < productResourceParameters.PriceLessThan);
                }

                if (productResourceParameters.PriceGreaterThan is not null)
                {
                    query = query.Where(x => x.Price > productResourceParameters.PriceGreaterThan);
                }

                var products = query.ToList();

                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

                return productDtos;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Product and ProductDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database error occured while fetching products.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while fetching products.", ex.Message);
                throw;
            }
        }

        public ProductDto? GetProductById(int id)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == id);

                var productDto = _mapper.Map<ProductDto>(product);

                return productDto;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Product and ProductDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError($"Database error occured while fetching product with id: {id}.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while fetching product with id: {id}.", ex.Message);
                throw;
            }
        }

        public ProductDto CreateProduct(ProductForCreateDto productToCreate)
        {
            try
            {
                var productEntity = _mapper.Map<Product>(productToCreate);

                _context.Products.Add(productEntity);
                _context.SaveChanges();

                var productDto = _mapper.Map<ProductDto>(productEntity);

                return productDto;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Product and ProductDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database error occured while creating new product.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while creating new product.", ex.Message);
                throw;
            }
        }

        public void UpdateProduct(ProductForUpdateDto productToUpdate)
        {
            try
            {
                var productEntity = _mapper.Map<Product>(productToUpdate);

                _context.Products.Update(productEntity);
                _context.SaveChanges();
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"There was an error mapping between Product and ProductDto", ex.Message);
                throw;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database error occured while updating product.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while updating product.", ex.Message);
                throw;
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == id);
                if (product is not null)
                {
                    _context.Products.Remove(product);
                }
                _context.SaveChanges();
            }
            catch (DbException ex)
            {
                _logger.LogError($"Database error occured while deleting product with id: {id}.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while deleting product with id: {id}.", ex.Message);
                throw;
            }
        }
    }
}
