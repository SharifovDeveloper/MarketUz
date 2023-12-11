using MarketUz.Domain.Entities;
using MarketUz.Domain.Interfaces.Repositories;

namespace MarketUz.Infrastructure.Persistence.Repositories
{
    public class SaleItemRepository : RepositoryBase<SaleItem>, ISaleItemRepository
    {
        public SaleItemRepository(MarketUzDbContext context) 
            : base(context)
        { }

    
    }
}
