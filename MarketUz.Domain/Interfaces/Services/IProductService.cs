
using MarketUz.Domain.DTOs.Product;
using MarketUz.Domain.Pagination;
using MarketUz.ResourceParameters;

namespace MarketUz.Domain.Interfaces.Services;

public interface IProductService
{
    PaginatedList<ProductDto> GetProducts(ProductResourceParameters productResourceParameters);
    ProductDto? GetProductById(int id);
    ProductDto CreateProduct(ProductForCreateDto productToCreate);
    void UpdateProduct(ProductForUpdateDto productToUpdate);
    void DeleteProduct(int id);
}
