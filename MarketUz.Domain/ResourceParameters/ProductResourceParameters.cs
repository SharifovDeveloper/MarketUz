namespace MarketUz.ResourceParameters
{
    public class ProductResourceParameters
    {
        public int? CategoryId { get; set; }
        public string? SearchString { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceLessThan { get; set; }
        public decimal? PriceGreaterThan { get; set; }
    }
}
