using DiyorMarket.Domain.Common;

namespace DiyorMarket.Domain.Entities
{
    public class SupplyItem : EntityBase
    {
     
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int SupplyId { get; set; }
        public Supply Supply { get; set; }
    }
}