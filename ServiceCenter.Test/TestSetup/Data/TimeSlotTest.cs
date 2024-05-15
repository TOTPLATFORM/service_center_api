using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class TimeSlotTest
{
    public static void AddTimeSlot(this ServiceCenterBaseDbContext context)
    {
        context.TimeSlots.AddRange(
        new TimeSlot
        {
            Id = 1,
            Day = "Monday",
            StartTime = new TimeOnly(02, 30, 10),
            EndTime = new TimeOnly(03, 30, 12)
        },
        new TimeSlot
        {
            Id = 3,
            Day = "Sunday",
            StartTime = new TimeOnly(02, 30, 20),
            EndTime = new TimeOnly(03, 30, 30) 
        }
        ); 
    }
}
