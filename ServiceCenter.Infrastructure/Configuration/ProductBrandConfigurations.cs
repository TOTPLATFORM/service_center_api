using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;

public class ProductBrandConfigurations : IEntityTypeConfiguration<ProductBrand>
{
    public void Configure(EntityTypeBuilder<ProductBrand> builder)
    {
        builder.Property(T => T.BrandName)
               .IsRequired()
               .HasColumnType("varchar")
               .HasMaxLength(50);

        builder.Property(T => T.BrandDescription)
               .IsRequired()
               .HasColumnType("varchar")
               .HasMaxLength(100);

        builder.Property(T => T.CountryOfOrigin)
              .IsRequired(); 

        builder.Property(T => T.FoundedYear)
               .IsRequired();
    }
}
