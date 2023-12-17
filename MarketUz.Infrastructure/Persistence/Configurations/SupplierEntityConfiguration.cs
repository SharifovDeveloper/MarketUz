using MarketUz.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketUz.Infrastructure.Persistence.Configurations
{
    internal class SupplierEntityConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable(nameof(Supplier));

            builder.HasKey(sp => sp.Id);

            builder.Property(sp => sp.FirstName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(sp => sp.LastName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(sp => sp.PhoneNumber)
                .HasMaxLength(255);
        }
    }


}

