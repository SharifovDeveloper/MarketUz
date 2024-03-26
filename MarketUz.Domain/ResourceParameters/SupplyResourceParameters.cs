namespace MarketUz.Domain.ResourceParameters
{
    public class SupplyResourceParameters : ResourceParametersBase
    {
        public int? SupplierId { get; set; }
        public DateTime? SupplyDate { get; set; }
        public override string OrderBy { get; set; } = "id";

    }
}
