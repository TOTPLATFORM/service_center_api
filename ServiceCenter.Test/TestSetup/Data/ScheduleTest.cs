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
            DayOfWeek = 0,
            StartTime = new TimeOnly(02, 00, 00),
            EndTime = new TimeOnly(03, 30, 00),
            ServiceId = 1,   
        },
        new Schedule
        {
            Id = 2,
            DayOfWeek = 0,
            StartTime = new TimeOnly(04, 00, 00),
            EndTime = new TimeOnly(07, 30, 00),
            ServiceId = 1,
        }
        ); ;
    }
    
}
