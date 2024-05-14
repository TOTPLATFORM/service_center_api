using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class ContractTest
{
    public static void AddContract(this ServiceCenterBaseDbContext context)
    {
        context.Contracts.AddRange(
        new Contract
        {
            Id = 1,
            Duration= DateOnly.Parse("3/11/2024"),
            ServicePackageId=1
        },
        new Contract
        {
            Id = 2,
            Duration = DateOnly.Parse("3/11/2024"),
            ServicePackageId = 1
        }
        );
    }
}
