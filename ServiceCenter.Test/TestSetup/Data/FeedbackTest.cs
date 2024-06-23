using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class FeedbackTest
{
    public static void AddFeedback(this ServiceCenterBaseDbContext context)
    {
        context.Feedbacks.AddRange(
        new Feedback
        {
            Id = 1,
            FeedbackDescription = "Desc1",
        },
        new Feedback
        {
            Id = 2,
            FeedbackDescription = "Desc2",
        }
        );
    }
}
