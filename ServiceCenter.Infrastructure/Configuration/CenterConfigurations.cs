using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;

public class CenterConfigurations : IEntityTypeConfiguration<Center>
{
	public void Configure(EntityTypeBuilder<Center> builder)
	{
		builder.Property(p => p.CenterName)
			.IsRequired()
			.HasMaxLength(50);

		builder.Property(p => p.OpeningHours)
			.IsRequired();

		builder.Property(p => p.Specialty)
			.IsRequired()
			.HasMaxLength(100);

	}

}
