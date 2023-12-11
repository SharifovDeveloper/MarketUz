namespace DiyorMarket.Domain.Interfaces.Repositories
{
    public interface ICommonRepository
    {
        public IProductRepository Product { get; }
        public ICategoryRepository Category { get; }

        public int SaveChanges();
    }
}
