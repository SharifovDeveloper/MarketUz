using MarketUz.Domain.DTOs.Category;
using MarketUz.Domain.DTOs.SaleItem;
using MarketUz.Domain.DTOs.SupplyItem;

namespace MarketUz.Domain.DTOs.Product
{
    public record ProductDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal SalePrice { get; init; }
        public decimal SupplyPrice { get; init; }
        public DateTime ExpireDate { get; init; }
        public int QuantityInStock { get; set; }
        public int LowQuantityAmount { get; set; }
        public CategoryDto Category { get; init; }
        public ICollection<SaleItemDto> SaleItems { get; init; }
        public ICollection<SupplyItemDto> SupplyItems { get; init; }
    }
}
