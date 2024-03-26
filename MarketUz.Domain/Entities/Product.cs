using MarketUz.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MarketUz.Domain.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpireDate { get; set; }
        [Range(0, int.MaxValue)]
        public int QuantityInStock { get; set; }
        [Range(0, int.MaxValue)]
        public int LowQuantityAmount { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public virtual ICollection<SaleItem> SaleItems { get; set; }
        public virtual ICollection<SupplyItem> SupplyItems { get; set; }
    }
}