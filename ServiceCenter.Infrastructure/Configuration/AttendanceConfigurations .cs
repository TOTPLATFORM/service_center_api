using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;
public class AttendanceConfigurations : IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {
        builder.Property(p => p.AttendanceDate)
            .IsRequired();

        builder.Property(p => p.ClockInTime)
            .IsRequired();

        builder.Property(p => p.EmployeeId)
            .IsRequired();
    }
}