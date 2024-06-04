using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class SubscriptionTest
{
    public static void AddSubscription(this ServiceCenterBaseDbContext context)
    {
        context.Subscriptions.AddRange(
        new Subscription
        {
            Id = 1,
            Duration= DateOnly.Parse("3/11/2024"),
            ServicePackageId=1
        },
        new Subscription
        {
            Id = 2,
            Duration = DateOnly.Parse("3/11/2024"),
            ServicePackageId = 1
        }
        );
    }
}
