using AutoMapper;
using DiyorMarket.Domain.Interfaces.Services;
using MarketUz.Domain.DTOs.Product;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Exceptions;
using MarketUz.Domain.Pagination;
using MarketUz.Infrastructure.Persistence;
using MarketUz.ResourceParameters;
using Microsoft.Extensions.Logging;
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

        public PaginatedList<ProductDto> GetProducts(ProductResourceParameters productResourceParameters)
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

            if (!string.IsNullOrEmpty(productResourceParameters.OrderBy))
            {
                query = productResourceParameters.OrderBy.ToLowerInvariant() switch
                {
                    "name" => query.OrderBy(x => x.Name),
                    "namedesc" => query.OrderByDescending(x => x.Name),
                    "description" => query.OrderBy(x => x.Description),
                    "descriptiondesc" => query.OrderByDescending(x => x.Description),
                    "price" => query.OrderBy(x => x.Price),
                    "pricedesc" => query.OrderByDescending(x => x.Price),
                    "expiredate" => query.OrderBy(x => x.ExpireDate),
                    "expiredatedesc" => query.OrderByDescending(x => x.ExpireDate),
                    _ => query.OrderBy(x => x.Name),
                };
            }

            var products = query.ToPaginatedList(productResourceParameters.PageSize, productResourceParameters.PageNumber);
            // var products = query.ToList();
            var productDtos = _mapper.Map<List<ProductDto>>(products);

            return new PaginatedList<ProductDto>(productDtos, products.TotalCount, products.CurrentPage, products.PageSize);

        }

        public ProductDto? GetProductById(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product is null)
            {
                throw new EntityNotFoundException($"Product with id: {id} not found");
            }
            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;

        }

        public ProductDto CreateProduct(ProductForCreateDto productToCreate)
        {

            var productEntity = _mapper.Map<Product>(productToCreate);

            _context.Products.Add(productEntity);
            _context.SaveChanges();

            var productDto = _mapper.Map<ProductDto>(productEntity);

            return productDto;

        }

        public void UpdateProduct(ProductForUpdateDto productToUpdate)
        {

            var productEntity = _mapper.Map<Product>(productToUpdate);

            _context.Products.Update(productEntity);
            _context.SaveChanges();

        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product is not null)
            {
                _context.Products.Remove(product);
            }
            _context.SaveChanges();

        }
    }
}
