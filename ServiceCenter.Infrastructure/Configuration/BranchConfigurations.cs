using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;

public class BranchConfigurations : IEntityTypeConfiguration<Branch>
{
	public void Configure(EntityTypeBuilder<Branch> builder)
	{
		builder.Property(p => p.BranchName)
		.IsRequired()
		.HasMaxLength(50);
		builder.Property(p => p.BranchPhoneNumber)
		.IsRequired()
		.HasMaxLength(50);
		builder.Property(p => p.Address)
		.IsRequired()
		.HasMaxLength(50);
		builder.Property(p => p.EmailAddress)
		.IsRequired()
		.HasMaxLength(50);
	
	}
}
