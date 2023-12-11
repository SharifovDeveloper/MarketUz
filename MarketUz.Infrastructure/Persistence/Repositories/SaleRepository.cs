using MarketUz.Domain.Entities;
using MarketUz.Domain.Interfaces.Repositories;

namespace MarketUz.Infrastructure.Persistence.Repositories
{
    public class SaleRepository : RepositoryBase<Sale>, ISaleRepository
    {
        public SaleRepository(MarketUzDbContext context)
            : base(context) { }

    }
}
