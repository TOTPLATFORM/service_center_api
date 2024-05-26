using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static  class CampaginTest
{
    public static void AddCampagin(this ServiceCenterBaseDbContext context)
    {
        context.Campagins.AddRange(
            new Campagin
            {
                Id = 3,
                CampaginName = "A",
                CampaginDescription = "B",
                Status = CampaginStatus.Active,
                Budget=2,
                Goals="Goal1",
                StartDate = DateOnly.Parse("2000/12/30"),
                EndDate = DateOnly.Parse("2000/12/30"),
            },
            new Campagin
            {
                Id = 5,
                CampaginName = "A",
                CampaginDescription = "B",
                Status = CampaginStatus.Active,
                Budget = 3,
                Goals = "Goal2",
                StartDate = DateOnly.Parse("2000/12/30"),
                EndDate = DateOnly.Parse("2000/12/30"),
            });
    }
}
