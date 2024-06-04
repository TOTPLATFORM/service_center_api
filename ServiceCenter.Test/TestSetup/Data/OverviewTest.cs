using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static  class OverviewTest
{
    public static void AddOverview(this ServiceCenterBaseDbContext context)
    {
        context.Overviews.AddRange(
        new Report
        {
            Id = 1,
            Task = "task1",
         Priority = "priority1",
           Status = Status.Approved,
            DueDate = DateTime.Parse("3/11/2024"),
            SalesId= "53ae72a7-589e-4f0b-81ed-40381"
        },
        new Report
        {
            Id = 2,
            Task = "task2",
            Priority = "priority2",
            Status = Status.Approved,
            DueDate = DateTime.Parse("3/11/2024"),
            SalesId = "53ae72a7-589e-4f0b-81ed-40381"
        }
        );
    }
}
