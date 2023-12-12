using MarketUz.Domain.Common;

namespace MarketUz.Domain.Entities
{
    public class Supply : EntityBase
    {

        public DateTime SupplyDate { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public virtual ICollection<SupplyItem> SupplyItems { get; set; }
    }
}
