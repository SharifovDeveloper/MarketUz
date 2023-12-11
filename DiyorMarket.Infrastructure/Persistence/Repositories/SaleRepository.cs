using DiyorMarket.Domain.Entities;
using DiyorMarket.Domain.Interfaces.Repositories;

namespace DiyorMarket.Infrastructure.Persistence.Repositories
{
    public class SaleRepository : RepositoryBase<Sale>, ISaleRepository
    {
        public SaleRepository(DiyorMarketDbContext context)
            : base(context) { }

    }
}
