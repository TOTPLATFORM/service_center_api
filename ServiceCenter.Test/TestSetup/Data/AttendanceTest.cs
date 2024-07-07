using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class AttendanceTest
{
    public static void AddAttendance(this ServiceCenterBaseDbContext context)
    {
        context.Attendances.AddRange(
        new Attendance
        {
            Id = 1,
            ClockInTime = TimeOnly.Parse("10:30:00"),
            ClockOutTime = TimeOnly.Parse("12:30:00"),
            AttendanceDate = DateOnly.Parse("3/11/2024"),

        },
        new Attendance
        {
            Id = 2,
            ClockInTime = TimeOnly.Parse("10:30:00"),
            ClockOutTime = TimeOnly.Parse("12:30:00"),
            AttendanceDate = DateOnly.Parse("3/11/2024"),
        }
        );
    }
}