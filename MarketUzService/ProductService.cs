using AutoMapper;
using MarketUz.Domain.DTOs.Product;
using MarketUz.Domain.Entities;
using MarketUz.Domain.Interfaces.Services;
using MarketUz.Domain.Pagination;
using MarketUz.Domain.Responses;
using MarketUz.Infrastructure.Persistence;
using MarketUz.ResourceParameters;
using Microsoft.EntityFrameworkCore;

namespace MarketUz.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly MarketUzDbContext _context;

        public ProductService(IMapper mapper, MarketUzDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public GetBaseResponse<ProductDto> GetProducts(ProductResourceParameters productResourceParameters)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (productResourceParameters.ExpireDate is not null)
            {
                query = query.Where(x => x.ExpireDate.Date == productResourceParameters.ExpireDate.Value.Date);
            }

            if (productResourceParameters.CategoryId is not null && productResourceParameters.CategoryId != 0)
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

            foreach (var product in products)
            {
                product.Category = _context.Categories.FirstOrDefault(x => x.Id == product.CategoryId);
            }

            var productDtos = _mapper.Map<List<ProductDto>>(products);

            var paginatedResult = new PaginatedList<ProductDto>(productDtos.ToList(), products.TotalCount, products.CurrentPage, products.PageSize);

            return paginatedResult.ToResponse();

        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            var products = _context.Products
                .Include(x => x.Category)
                .AsSplitQuery()
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Category.Name)
                .ToList();

            return _mapper.Map<IEnumerable<ProductDto>>(products) ?? Enumerable.Empty<ProductDto>();
        }

        public ProductDto? GetProductById(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(x => x.Id == id);

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
