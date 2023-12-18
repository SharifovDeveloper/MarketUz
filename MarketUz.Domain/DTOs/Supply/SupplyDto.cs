using MarketUz.Domain.DTOs.SupplyItem;

namespace MarketUz.Domain.DTOs.Supply
{
    public class SupplyDto
    {
        public int Id { get; set; }
        public DateTime SupplyDate { get; set; }
        public int SupplierId { get; set; }
        public virtual ICollection<SupplyItemDto> SupplItems { get; set; }
    }

}
