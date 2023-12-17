
using MarketUz.Domain.DTOs.Product;
using MarketUz.ResourceParameters;

namespace DiyorMarket.Domain.Interfaces.Services;

public interface IProductService
{
    IEnumerable<ProductDto> GetProducts(ProductResourceParameters productResourceParameters);
    ProductDto? GetProductById(int id);
    ProductDto CreateProduct(ProductForCreateDto productToCreate);
    void UpdateProduct(ProductForUpdateDto productToUpdate);
    void DeleteProduct(int id);
}