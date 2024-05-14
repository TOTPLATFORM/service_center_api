using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(T => T.ProductName)
               .IsRequired()
               .HasColumnType("varchar")
			   .HasMaxLength(50);

		builder.Property(T => T.ProductDescription)
              .IsRequired()
              .HasColumnType("varchar")
			  .HasMaxLength(50);

		builder.Property(T => T.ProductPrice)
              .IsRequired();

        builder.Property(T => T.ProductCategoryId)
              .IsRequired();

        builder.Property(T => T.ProductBrandId)
              .IsRequired();
    }
}
