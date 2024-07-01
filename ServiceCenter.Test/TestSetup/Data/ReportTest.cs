using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class ReportTest
{
    public static void AddReport(this ServiceCenterBaseDbContext context)
    {
        context.Reports.AddRange(
        new Report
        {
            Id = 1,
            Task="Report1",
            Priority="Priority1",
            Status=ReportStatus.Good,

        },
        new Report
        {
            Id = 2,
            Task = "Report1",
            Priority = "Priority1",
            Status = ReportStatus.Good,
        }
        );
    }
}
