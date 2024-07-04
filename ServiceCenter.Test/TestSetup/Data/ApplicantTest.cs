using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class ApplicantTest
{
    public static void AddApplicant(this ServiceCenterBaseDbContext context)
    {
        context.Applicants.AddRange(
        new Applicant
        {
            Id = "78ty72a7-589e-4f0b-81ed-40389f683610",
            Email = "mariamabdeen299@gmail.com",
            PhoneNumber = "01021432813",
            UserName = "mariamabdeen",
            DepartmentId = 5

        },
         new Applicant
         {
             Id = "45yi72a7-589e-4f0b-81ed-40389f683020",
             Email = "mariamabdeen299@gmail.com",
             PhoneNumber = "01021432813",
             UserName = "mariamabdeen",
             DepartmentId = 3
         }
        );
    }
}