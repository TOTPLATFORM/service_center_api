using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;

public class Feedbackconfigurations : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.Property(T => T.FeedbackDescription)
             .IsRequired()
			 .HasColumnType("varchar")
             .HasMaxLength(100);

        builder.Property(T => T.FeedbackCategory)
            .IsRequired();

		builder.Property(T => T.FeedbackDate)
              .IsRequired();
        
    }
}
