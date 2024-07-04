using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class SalaryTest
{
    public static void AddSalary(this ServiceCenterBaseDbContext context)
    {
        context.Salaries.AddRange(
        new Salary
        {
            Id = 1,
            Bonus = 200,
            Deduction = 300,
            EmployeeId = "78ty72a7-589e-4f0b-81ed-40389f683616"
        },
        new Salary
        {
            Id = 2,
            Bonus = 200,
            Deduction = 300,
            EmployeeId = "45yi72a7-589e-4f0b-81ed-40389f683027"
        }
        );
    }
}