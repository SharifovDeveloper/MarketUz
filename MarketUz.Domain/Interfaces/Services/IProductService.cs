using MarketUz.Domain.Responses;
using MarketUz.ResourceParameters;
using Inflow.Core.Product;

namespace MarketUz.Domain.Interfaces.Services;

public interface IProductService
{
    IEnumerable<ProductDto> GetAllProducts();
    GetBaseResponse<ProductDto> GetProducts(ProductResourceParameters productResourceParameters);
    ProductDto? GetProductById(int id);
    ProductDto CreateProduct(ProductForCreateDto productToCreate);
    void UpdateProduct(ProductForUpdateDto productToUpdate);
    void DeleteProduct(int id);
}
