using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;

public class ServiceCategoryConfigurations : IEntityTypeConfiguration<ServiceCategory>
{
    public void Configure(EntityTypeBuilder<ServiceCategory> builder)
    {
        builder.Property(T => T.ServiceCategoryName)
               .IsRequired()
               .HasColumnType("varchar")
			   .HasMaxLength(50);

		builder.Property(T=>T.ServiceCategoryDescription)
              .IsRequired()
              .HasColumnType("varchar")
			  .HasMaxLength(50);

	}
}
