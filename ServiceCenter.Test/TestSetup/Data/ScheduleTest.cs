using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class ScheduleTest
{
    public static void AddSchedule(this ServiceCenterBaseDbContext context)
    {
        context.Schedules.AddRange(
        new Schedule
        {
            Id = 1,
            DayOfWeek = DayOfWeek.Sunday,
            StartTime =TimeOnly.Parse("10:30:00"),
            EndTime = TimeOnly.Parse("10:30:00"),
            ServiceProviderId = "0d133c1a-804f-4548-8f7e-8c3f504844e0"
        },
        new Schedule
        {
            Id = 2,
            DayOfWeek = DayOfWeek.Monday,
            StartTime = TimeOnly.Parse("10:30:00"),
            EndTime = TimeOnly.Parse("10:30:00"),
            ServiceProviderId = "0d133c1a-804f-4548-8f7e-8c3f504844e0"
        }
        ); ;
    }
}
