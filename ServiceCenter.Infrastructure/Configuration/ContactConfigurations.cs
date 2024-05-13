﻿using Microsoft.EntityFrameworkCore;
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
		builder.Property(p => p.ContactFirstName)
		.HasColumnType("varchar")
		.IsRequired()
		.HasMaxLength(50);

		builder.Property(p => p.ContactLastName)
		.HasColumnType("varchar")
		.IsRequired()
		.HasMaxLength(50);

		builder.Property(p => p.Gender)
			.HasColumnType("varchar")
			.HasMaxLength(6);

	}
}
