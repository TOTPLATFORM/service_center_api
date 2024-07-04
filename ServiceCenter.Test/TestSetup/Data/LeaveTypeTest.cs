using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class LeaveTypeTest
{
    public static void AddLeaveType(this ServiceCenterBaseDbContext context)
    {
        context.LeaveTypes.AddRange(
        new LeaveType
        {
            Id = 1,

            TypeName = "Sickkk"

        },
        new LeaveType
        {
            Id = 2,

            TypeName = "Sickkk"

        }
        );
    }

}