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
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.SaleNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.SaleDate)
                .IsRequired();

            builder.Property(x => x.CustomerId)
                .IsRequired();

            builder.Property(x => x.CustomerName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.BranchId)
                .IsRequired();

            builder.Property(x => x.BranchName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.IsCancelled)
                .IsRequired();

            builder.Ignore(x => x.TotalAmount);

            builder
                .HasMany(x => x.Items)
                .WithOne()
                .HasForeignKey("SaleId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
