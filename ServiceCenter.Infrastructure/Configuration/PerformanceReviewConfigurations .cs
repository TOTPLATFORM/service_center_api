using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;
public class PerformanceReviewConfigurations : IEntityTypeConfiguration<PerformanceReview>
{
    public void Configure(EntityTypeBuilder<PerformanceReview> builder)
    {
        builder.Property(p => p.EmployeeId)
            .IsRequired();

        builder.Property(p => p.PerformanceDetails)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(p => p.Comments)
            .HasMaxLength(250)
            .IsRequired();
    }
}