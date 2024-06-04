using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class RatingServiceTest
{
    public static void AddRatingService(this ServiceCenterBaseDbContext context)
    {
        context.RatingServices.AddRange(
        new RatingService
        {
            Id = 1,
            RatingValue = 1
            
        },
        new RatingService
        {
            Id = 2,
            RatingValue = 3
        }
        );
    }
}
