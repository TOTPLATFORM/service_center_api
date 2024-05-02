using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class CenterTest
{
    public static void AddCenter(this ServiceCenterBaseDbContext context)
    {
        context.Centers.AddRange(
        new Center
        {
            Id = 1,
            OpeningHours = 10,
            Specialty="center1",

        },
        new Center
        {

            Id = 2,
            OpeningHours = 12,
            Specialty = "center2",
        }
        ); 
    }
}
