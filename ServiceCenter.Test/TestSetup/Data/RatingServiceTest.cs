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
    public static void AddRating(this ServiceCenterBaseDbContext context)
    {
        context.Ratings.AddRange(
        new Rating
        {
            Id = 1,
            RatingValue = 1

        },
        new Rating
        {
            Id = 2,
            RatingValue = 3
        }
        );
    }
}
