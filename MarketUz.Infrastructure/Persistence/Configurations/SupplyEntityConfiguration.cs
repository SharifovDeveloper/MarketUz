using MarketUz.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketUz.Infrastructure.Persistence.Configurations
{
    internal class SupplyEntityConfiguration : IEntityTypeConfiguration<Supply>
    {
        public void Configure(EntityTypeBuilder<Supply> builder)
        {
            builder.ToTable(nameof(Supply));

            builder.HasKey(su => su.Id);

            builder.Property(su => su.SupplyDate)
                .HasColumnType("date");

            builder.HasOne(su => su.Supplier)
                .WithMany(sp => sp.Supplies)
                .HasForeignKey(su => su.SupplierId);
        }

    }
}
