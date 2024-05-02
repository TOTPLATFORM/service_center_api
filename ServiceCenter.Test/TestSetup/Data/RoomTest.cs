using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using ServiceCenter.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class RoomTest
{
    public static void AddRoom(this ServiceCenterBaseDbContext context)
    {
        context.Rooms.AddRange(
        new Room
        {
            Id = 1,
            Availability = true,           
            RoomNumber = 1,
            CenterId = 1
        },
        new Room
        {
            Id = 2,
            Availability = true,
           RoomNumber = 10,
           CenterId= 1
        }
        );
    }
}
