using MarketUz.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MarketUz.Infrastructure.Persistence
{
    public class MarketUzDbContext : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        public MarketUzDbContext(DbContextOptions<MarketUzDbContext> options)
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
