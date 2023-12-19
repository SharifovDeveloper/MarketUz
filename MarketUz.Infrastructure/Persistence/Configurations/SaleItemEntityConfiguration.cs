using MarketUz.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketUz.Infrastructure.Persistence.Configurations
{
    internal class SaleItemEntityConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable(nameof(SaleItem));

            builder.HasKey(st => st.Id);

            builder.Property(st => st.Quantity);

            builder.Property(st => st.UnitPrice)
                .HasColumnType("money");

            builder.HasOne(st => st.Product)
                .WithMany(p => p.SaleItems)
                .HasForeignKey(st => st.ProductId);

            builder.HasOne(st => st.Sale)
                .WithMany(s => s.SaleItems)
                .HasForeignKey(st => st.SaleId);
        }

    }
}
