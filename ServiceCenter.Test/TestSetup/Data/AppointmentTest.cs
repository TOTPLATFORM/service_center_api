using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class AppointmentTest
{
    public static void AddAppointment(this ServiceCenterBaseDbContext context)
    {
        context.Appointments.AddRange(
        new Appointment
        {
            Id = 1
        },
        new Appointment
        {
            Id = 3
        }
        );
    }
}
