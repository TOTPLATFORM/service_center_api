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
            TimeSlotId = 1,
            EmployeeId= "53ae72a7-589e-4f0b-81ed-4038169498",
            AppointmentId=1
        },
        new Schedule
        {
            Id = 2,
            TimeSlotId = 2,
            EmployeeId = "53ae72a7-589e-4f0b-81ed-4038169498",
            AppointmentId = 1
        }
        );
    }
}
