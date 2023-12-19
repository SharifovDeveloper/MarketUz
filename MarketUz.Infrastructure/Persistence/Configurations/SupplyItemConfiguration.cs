using MarketUz.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketUz.Infrastructure.Persistence.Configurations
{
    internal class SupplyItemConfiguration : IEntityTypeConfiguration<SupplyItem>
    {
        public void Configure(EntityTypeBuilder<SupplyItem> builder)
        {
            builder.ToTable(nameof(SupplyItem));

            builder.HasKey(spi => spi.Id);

            builder.Property(spi => spi.Quantity);

            builder.Property(spi => spi.UnitPrice)
                .HasColumnType("money");

            builder.HasOne(spi => spi.Product)
                .WithMany(p => p.SupplyItems)
                .HasForeignKey(spi => spi.ProductId);

            builder.HasOne(spi => spi.Supply)
                .WithMany(su => su.SupplyItems)
                .HasForeignKey(spi => spi.SupplyId);

        }
    }

}

