using DiyorMarket.Domain.Entities;
using DiyorMarket.Domain.Interfaces.Repositories;

namespace DiyorMarket.Infrastructure.Persistence.Repositories
{
    public class SupplyRepository : RepositoryBase<Supply>, ISupplyRepository
    {
        public SupplyRepository(DiyorMarketDbContext context)
            : base(context) { }

     
    }
}
