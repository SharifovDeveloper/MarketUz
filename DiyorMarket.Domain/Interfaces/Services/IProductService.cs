using DiyorMarket.Domain.Entities;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface IProductService
    {
        public IEnumerable<Product> GetAll();
        public Product GetById(int id);
        public Product Create(Product product);
        public void Update(Product product);
        public void Delete(int id);
    }
}
