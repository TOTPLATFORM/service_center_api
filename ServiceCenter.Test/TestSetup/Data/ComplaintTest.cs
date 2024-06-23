using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class ComplaintTest
{
    public static void AddComplaint(this ServiceCenterBaseDbContext context)
    {
        context.Complaints.AddRange(
        new Complaint
        {
            Id = 9,
            ComplaintDescription = "Desc1",
            ComplaintStatus = Status.Approved
        },
        new Complaint
        {
            Id = 6,
            ComplaintDescription = "Desc2",
            ComplaintStatus = Status.Cancelled
        }
        );
    }
}
