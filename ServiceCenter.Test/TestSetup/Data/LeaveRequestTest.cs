using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class LeaveRequestTest
{
    public static void AddLeaveRequest(this ServiceCenterBaseDbContext context)
    {
        context.LeaveRequests.AddRange(
        new LeaveRequest
        {
            Id = 1,
            StartDate = DateOnly.Parse("3/11/2024"),
            EndDate = DateOnly.Parse("7/11/2024"),
            LeaveTypeId = 1

        },
        new LeaveRequest
        {
            Id = 2,
            StartDate = DateOnly.Parse("3/11/2024"),
            EndDate = DateOnly.Parse("4/11/2024"),
            LeaveTypeId = 1

        }
        );
    }
}