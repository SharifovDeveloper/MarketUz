using MarketUz.Domain.Common;

namespace MarketUz.Domain.Entities
{
    public class Product : EntityBase
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpireDate { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public virtual ICollection<SaleItem> SaleItems { get; set; }
        public virtual ICollection<SupplyItem> SupplyItems { get; set; }
    }
}