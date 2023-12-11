using DiyorMarket.Domain.Common;

namespace DiyorMarket.Domain.Entities
{
    public class Customer:EntityBase
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
