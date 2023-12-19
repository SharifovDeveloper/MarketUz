using DiyorMarket.Domain.DTOs.Sale;

namespace MarketUz.Domain.DTOs.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<SaleDto> Sales { get; set; }
    }


}
