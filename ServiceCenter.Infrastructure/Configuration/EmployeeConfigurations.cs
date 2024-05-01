using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;

public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
{
	public void Configure(EntityTypeBuilder<Employee> builder)
	{
		builder.Property(T => T.FirstName)
			.HasColumnType("varchar")
			.HasMaxLength(30);

		builder.Property(T => T.LastName)
			.HasColumnType("varchar")
			.HasMaxLength(30);

		builder.Property(T => T.Gender)
			.HasColumnType("varchar")
			.HasMaxLength(6);
	}
}