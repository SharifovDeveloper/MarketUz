using MarketUz.Domain.Interfaces.Repositories;

namespace MarketUz.Infrastructure.Persistence.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private readonly MarketUzDbContext _context;

        private ICategoryRepository _category;
        public ICategoryRepository Category
        {
            get
            {
                _category ??= new CategoryRepository(_context);

                return _category;
            }
        }

        private IProductRepository _product;
        public IProductRepository Product
        {
            get
            {
                _product ??= new ProductRepository(_context);

                return _product;
            }
        }
        public CommonRepository(MarketUzDbContext context)
        {
            _context = context;

            _category = new CategoryRepository(context);
            _product = new ProductRepository(context);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

      
    }
}
