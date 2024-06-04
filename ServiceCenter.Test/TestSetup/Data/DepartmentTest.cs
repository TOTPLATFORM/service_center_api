using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class DepartmentTest
{
    public static void AddDepartment(this ServiceCenterBaseDbContext context)
    {
        context.Departments.AddRange(
            new Department
            {
                Id = 3,
                DepartmentName = "A",
               
            },
            new Department
            {
                Id = 5,
                DepartmentName = "B",
               
            });
    }
}
