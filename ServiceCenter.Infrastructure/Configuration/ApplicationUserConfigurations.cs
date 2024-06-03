using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
	public void Configure(EntityTypeBuilder<ApplicationUser> builder)
	{
		builder.UseTptMappingStrategy();

		builder.Property(T => T.FirstName)
			.HasColumnType("varchar")
			.HasMaxLength(30);

		builder.Property(T => T.LastName)
			.HasColumnType("varchar")
			.HasMaxLength(30);

		builder.Property(T => T.Gender)
			.HasColumnType("varchar")
			.HasMaxLength(20);

		builder.Property(T => T.DateOfBirth)
			   .IsRequired();
	}
}
