using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class BranchTest
{
    public static void AddBranch(this ServiceCenterBaseDbContext context)
    {
        context.Branches.AddRange(
        new Branch
        {
            Id = 1,
            BranchName="barnch1",
            BranchPhoneNumber="03265652",
            EmailAddress = "hagershabaan7@gmail.com",
            CenterId=1

        },
        new Branch
        {
            Id = 2,
            BranchName = "barnch2",
            BranchPhoneNumber = "03265652",
            EmailAddress = "hagershabaan8@gmail.com",
            CenterId = 1

        }
        );
    }
}
