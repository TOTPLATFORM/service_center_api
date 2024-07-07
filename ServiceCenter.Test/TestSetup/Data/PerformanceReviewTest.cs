using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class PerformanceReviewTest
{
    public static void AddPerformanceReview(this ServiceCenterBaseDbContext context)
    {
        context.PerformanceReviews.AddRange(
        new PerformanceReview
        {
            Id = 1,
            Comments = "any",
            PerformanceDetails = "any",
            PerformanceRating = 5,
            ReviewDate = DateOnly.Parse("3/11/2024")
        },
       new PerformanceReview
       {
           Id = 2,
           Comments = "any",
           PerformanceDetails = "any",
           PerformanceRating = 5,
           ReviewDate = DateOnly.Parse("3/11/2024")
       });
    }
}