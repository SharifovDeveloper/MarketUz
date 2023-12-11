using MarketUz.Domain.Entities;
using MarketUz.Domain.Interfaces.Repositories;

namespace MarketUz.Infrastructure.Persistence.Repositories
{
    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(MarketUzDbContext context)
            : base(context) { }
    }
}
