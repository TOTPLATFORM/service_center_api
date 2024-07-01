using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;

public class ContactConfigurations :IEntityTypeConfiguration<Contact>
{
	public void Configure(EntityTypeBuilder<Contact> builder)
	{
		builder.ToTable("Contacts");
		builder.Property(p => p.FirstName)
		.HasColumnType("varchar")
		.IsRequired()
		.HasMaxLength(50);

		builder.Property(p => p.LastName)
		.HasColumnType("varchar")
		.IsRequired()
		.HasMaxLength(50);

		builder.Property(p => p.Gender).IsRequired();

	}
}
