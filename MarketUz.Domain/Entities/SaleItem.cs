using MarketUz.Domain.Common;

namespace MarketUz.Domain.Entities
{
    public class SaleItem : EntityBase
    {

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int SaleId { get; set; }
        public Sale Sale { get; set; }
    }
}
