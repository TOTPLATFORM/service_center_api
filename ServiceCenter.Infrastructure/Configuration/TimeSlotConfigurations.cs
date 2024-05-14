using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;

public class TimeSlotConfigurations : IEntityTypeConfiguration<TimeSlot>
{
	public void Configure(EntityTypeBuilder<TimeSlot> builder)
	{
		builder.Property(T => T.Day)
		  .IsRequired()
		  .HasColumnType("varchar")
		  .HasMaxLength(10);

	}
}
