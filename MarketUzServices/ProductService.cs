using AutoMapper;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Product;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiyorMarket.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly ICommonRepository _repository;
        private readonly ILogger<CustomerService> _logger;

        public ProductService(IMapper mapper, ICommonRepository repository, ILogger<CustomerService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public ProductDto CreateProduct(ProductForCreateDto product)
        {
            try
            {
                var productEntity = _mapper.Map<Product>(product);
                var createdEntity = _repository.Product.Create(productEntity);

                _repository.SaveChanges();

                return _mapper.Map<ProductDto>(productEntity);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Database error creating new customer ", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating new customer ", ex);
                throw;
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                _repository.Product.Delete(id);
                _repository.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error deleting product with id : {id}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting product with id : {id}", ex);
                throw;
            }
        }

        public ProductDto GetProductById(int id)
        {
            try
            {
                var product = _repository.Product.FindById(id);
                var productDto = _mapper.Map<ProductDto>(product);

                return productDto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error fetching product with id : {id}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching product with id : {id}", ex);
                throw;
            }
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            try
            {
                var products = _repository.Product.FindAll();
                var productDto = _mapper.Map<IEnumerable<ProductDto>>(products);

                return productDto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Database error fetching product ", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching product ", ex);
                throw;
            };
        }

        public void UpdateProduct(ProductForUpdateDto product)
        {
            try
            {
                var productEntity = _mapper.Map<Product>(product);
                _repository.Product.Update(productEntity);

                _repository.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error updating product with id : {product.Id}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating product with id : {product.Id}", ex);
                throw;
            }
        }
    }
}
