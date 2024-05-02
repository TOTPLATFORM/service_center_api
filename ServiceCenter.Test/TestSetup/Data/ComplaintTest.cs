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
            Id = 1,
            ComplaintCategory = "cat1",
            ComplaintDescription = "Desc1",
            AssignedTo ="Dep1",
            ComplaintStatus =Status.Approved,
            CustomerId = "53ae72a7-589e-4f0b-81ed-4038169498",
            ComplaintDate = DateOnly.Parse("3/11/2024"),
        },
        new Complaint
        {
            Id = 2,
            ComplaintCategory = "cat2",
            ComplaintDescription = "Desc2",
            CustomerId = "53ae72a7-589e-4f0b-81ed-40381",
            ComplaintDate = DateOnly.Parse("7/11/2024"),
            AssignedTo = "Dep2",
            ComplaintStatus = Status.Cancelled,
        }
        );
    }
}
