namespace MarketUz.Domain.ResourceParameters
{
    public class SaleResourceParameters : ResourceParametersBase
    {
        public int? CustomerId { get; set; }
        public DateTime? SaleDate { get; set; }
        public override string OrderBy { get; set; } = "id";
    }
}
