using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mouts.Challenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Infrastructure.Persistence.Configurations
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property<int>("SaleId")
                .IsRequired();

            builder.Property(x => x.ProductId)
                .IsRequired();

            builder.Property(x => x.ProductName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.UnitPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.DiscountPercentage)
                .HasPrecision(5, 2)
                .IsRequired();

            builder.Property(x => x.DiscountAmount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.IsCancelled)
                .IsRequired();

            builder.Ignore(x => x.GrossAmount);
            builder.Ignore(x => x.TotalAmount);
        }
    }
}