using DiyorMarket.Domain.Common;

namespace DiyorMarket.Domain.Entities
{
    public class Category : EntityBase
    {
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Product> Products { get; set; }

        public Category()
        {
            Products = new List<Product>();
        }
    }
}