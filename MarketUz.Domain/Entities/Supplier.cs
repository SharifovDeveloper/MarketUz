using MarketUz.Domain.Common;

namespace MarketUz.Domain.Entities
{
    public class Supplier : EntityBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string PhoneNumber { get; set; } = "+998949323212";
        public string? Company { get; set; }
        public virtual ICollection<Supply>? Supplies { get; set; }
    }
}