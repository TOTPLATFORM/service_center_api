using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;

public class ServicePackageConfiguration : IEntityTypeConfiguration<ServicePackage>
{
    public void Configure(EntityTypeBuilder<ServicePackage> builder)
    {
        builder.Property(T => T.PackageName)
            .IsRequired()
            .HasColumnType("varchar")
			.HasMaxLength(50);
        builder.Property(T => T.PackageDescription)
           .IsRequired()
           .HasColumnType("varchar");

		builder.Property(T => T.PackagePrice)
          .IsRequired();
    }
}
