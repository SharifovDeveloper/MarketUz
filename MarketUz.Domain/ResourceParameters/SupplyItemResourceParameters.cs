namespace MarketUz.Domain.ResourceParameters
{
    public class SupplyItemResourceParameters : ResourceParametersBase
    {
        public int? ProductId { get; set; }
        public int? SupplyId { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? UnitPriceLessThan { get; set; }
        public decimal? UnitPriceGreaterThan { get; set; }
        public override string OrderBy { get; set; } = "id";
    }
}
