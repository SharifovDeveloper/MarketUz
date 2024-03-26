using MarketUz.Domain.ResourceParameters;

namespace MarketUz.ResourceParameters
{
    public class ProductResourceParameters : ResourceParametersBase
    {
        public DateTime? ExpireDate { get; set; }
        public int? CategoryId { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceLessThan { get; set; }
        public decimal? PriceGreaterThan { get; set; }
        public override string OrderBy { get; set; } = "name";
    }
}
