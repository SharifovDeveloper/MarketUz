using DiyorMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiyorMarket.Infrastructure.Persistence.Configurations
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

