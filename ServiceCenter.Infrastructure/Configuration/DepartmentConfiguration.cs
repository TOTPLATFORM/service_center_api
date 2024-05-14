using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
	public void Configure(EntityTypeBuilder<Department> builder)
	{
		builder.Property(p => p.DepartmentName)
		.IsRequired()
		.HasMaxLength(50)
		.HasColumnType("varchar");

	}
}