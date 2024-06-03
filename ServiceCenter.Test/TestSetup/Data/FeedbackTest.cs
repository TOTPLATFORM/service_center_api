//using ServiceCenter.Domain.Entities;
//using ServiceCenter.Infrastructure.BaseContext;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ServiceCenter.Test.TestSetup.Data;

//public static class FeedbackTest
//{
//    public static void AddFeedback(this ServiceCenterBaseDbContext context)
//    {
//        context.Feedbacks.AddRange(
//        new Feedback
//        {
//            Id = 1,
//            FeedbackCategory = "cat1",
//            FeedbackDescription = "Desc1",
//            CustomerId = "53ae72a7-589e-4f0b-81ed-4038169498",
//            FeedbackDate = DateOnly.Parse("3/11/2024"),
//        },
//        new Feedback
//        {
//            Id = 2,
//            FeedbackCategory = "cat2",
//            FeedbackDescription = "Desc2",
//            CustomerId = "53ae72a7-589e-4f0b-81ed-40381",
//            FeedbackDate = DateOnly.Parse("7/11/2024"),
//        }
//        );
//    }
//    }
