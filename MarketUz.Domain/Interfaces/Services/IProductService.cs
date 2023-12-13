using DiyorMarket.Domain.DTOs.Product;

namespace DiyorMarket.Domain.Interfaces.Services;

public interface IProductService
{
    IEnumerable<ProductDto> GetProducts();
    ProductDto GetProductById(int id);
    ProductDto CreateProduct(ProductForCreateDto product);
    void UpdateProduct(ProductForUpdateDto product);
    void DeleteProduct(int id); 
}
