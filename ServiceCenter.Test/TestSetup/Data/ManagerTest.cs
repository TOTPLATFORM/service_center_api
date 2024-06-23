using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class ManagerTest
{
    public static void AddManager(this ServiceCenterBaseDbContext context)
    {
        context.Managers.AddRange(
        new Manager
        {
            Id = "0d133c1a-804f-4548-8f7e-8c3f504804e0",
            DateOfBirth = DateOnly.Parse("2000/12/30"),
            Email = "agershaban7@gmail.com",
            FirstName = "hager",
            LastName = "shaban",
            Gender = Gender.Female,
            PhoneNumber = "0621654984",
            Responsibilities="Task",
            HiringDate= DateOnly.Parse("2000/12/30"),
            Experience=10,
            WorkingHours=8,
            BranchId=1
        },
        new Manager
        {
            Id = "sOB316984865",
            DateOfBirth = DateOnly.Parse("2000/12/30"),
            Email = "agershaban7@gmail.com",
            FirstName = "hager",
            LastName = "shaban",
            Gender = Gender.Female,
            PhoneNumber = "0621654984",
            Responsibilities = "Task",
            HiringDate = DateOnly.Parse("2000/12/30"),
            Experience = 10,
            WorkingHours = 8,
            BranchId = 1
        }
        );
    }
}
