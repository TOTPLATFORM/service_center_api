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
            EmployeeId = "78ty72a7-589e-4f0b-81ed-40389f683616",
            ReviewDate = DateOnly.Parse("3/11/2024")
        },
       new PerformanceReview
       {
           Id = 2,
           Comments = "any",
           PerformanceDetails = "any",
           PerformanceRating = 5,
           EmployeeId = "45yi72a7-589e-4f0b-81ed-40389f683027",
           ReviewDate = DateOnly.Parse("3/11/2024")
       });
    }
}