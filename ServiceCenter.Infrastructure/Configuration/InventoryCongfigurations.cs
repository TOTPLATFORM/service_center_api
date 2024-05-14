using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;

public class InventoryCongfigurations : IEntityTypeConfiguration<Inventory>
{
	public void Configure(EntityTypeBuilder<Inventory> builder)
	{
		builder.Property(p => p.InventoryName)
			.IsRequired()
			.HasColumnType("varchar")
			.HasMaxLength(50);

		builder.Property(p => p.InventoryLocation)
			.IsRequired()
			.HasColumnType("varchar")
			.HasMaxLength(250);

		builder.Property(p => p.InventoryCapacity)
			.IsRequired();
	}

}
