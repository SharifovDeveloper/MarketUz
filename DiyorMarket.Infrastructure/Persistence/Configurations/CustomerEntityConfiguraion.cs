using DiyorMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiyorMarket.Infrastructure.Persistence.Configurations
{
    public class CustomerEntityConfiguraion : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable(nameof(Customer));

            builder.HasKey(cu => cu.Id);

            builder.Property(cu => cu.FirstName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(cu => cu.LastName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(cu => cu.PhoneNumber)
                .HasColumnType("phone");
        }

    }
}
