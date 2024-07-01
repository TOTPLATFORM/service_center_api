using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;
public class RecruitmentRecordConfigurations : IEntityTypeConfiguration<RecruitmentRecord>
{
    public void Configure(EntityTypeBuilder<RecruitmentRecord> builder)
    {
        builder.Property(p => p.ApplicantId)
            .IsRequired();

        builder.Property(p => p.DepartmentId)
            .IsRequired();

        builder.Property(p => p.Status)
            .IsRequired();
    }
}