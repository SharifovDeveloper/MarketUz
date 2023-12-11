using DiyorMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DiyorMarket.Infrastructure.Persistence
{
    public class DiyorMarketDbContext : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        public DiyorMarketDbContext(DbContextOptions<DiyorMarketDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
