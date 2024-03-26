namespace MarketUz.Domain.ResourceParameters
{
    public class SaleItemResourceParameters : ResourceParametersBase
    {
        public override string OrderBy { get; set; } = "id";
        public int? ProductId { get; set; }
        public int? SaleId { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? UnitPriceLessThan { get; set; }
        public decimal? UnitPriceGreaterThan { get; set; }
    }
}
