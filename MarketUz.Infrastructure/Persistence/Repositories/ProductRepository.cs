using MarketUz.Domain.Entities;
using MarketUz.Domain.Interfaces.Repositories;

namespace MarketUz.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(MarketUzDbContext context)
            : base(context) { }
    }
}
