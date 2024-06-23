using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class OverviewTest
{
    public static void AddOverview(this ServiceCenterBaseDbContext context)
    {
        context.Reports.AddRange(
        new Report
        {
            Id = 1,
            Task = "task1",
            Priority = "priority1",
            Status = ReportStatus.Good,
            DueDate = DateTime.Parse("3/11/2024"),
        },
        new Report
        {
            Id = 2,
            Task = "task2",
            Priority = "priority2",
            DueDate = DateTime.Parse("3/11/2024"),
        }
        );
    }
}
