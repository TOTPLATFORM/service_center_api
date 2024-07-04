using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class RecruitmentRecordTest
{
    public static void AddRecruitmentRecord(this ServiceCenterBaseDbContext context)
    {
        context.RecruitmentRecords.AddRange(
        new RecruitmentRecord
        {
            Id = 1,
            ApplicantId = "553ae72a7-589e-4f0b-81ed-40388754",
            DepartmentId = 3,
            Status = Status.Pending,
        },
        new RecruitmentRecord
        {
            Id = 2,
            ApplicantId = "553ae72a7-589e-4f0b-81ed-40388754",
            DepartmentId = 5,
            Status = Status.Pending,
        }
        );
    }
}