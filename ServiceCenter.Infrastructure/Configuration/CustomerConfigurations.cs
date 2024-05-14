using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;

public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
{
	public void Configure(EntityTypeBuilder<Customer> builder)
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

		builder.Property(p => p.BranchId)
			   .IsRequired();
	

	}
}
