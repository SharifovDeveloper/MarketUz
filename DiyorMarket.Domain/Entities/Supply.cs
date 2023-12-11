using DiyorMarket.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiyorMarket.Domain.Entities
{
    public class Supply : EntityBase
    {
        
        public DateTime SupplyDate { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public virtual ICollection<SupplyItem> SupplyItems { get; set; }
    }
}
