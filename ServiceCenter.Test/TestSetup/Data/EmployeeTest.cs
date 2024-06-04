//using ServiceCenter.Domain.Entities;
//using ServiceCenter.Infrastructure.BaseContext;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ServiceCenter.Test.TestSetup.Data;

//public static class EmployeeTest
//{
//    public static void AddEmployee(this ServiceCenterBaseDbContext context)
//    {
//        context.Employees.AddRange(
//        new Employee
//        {
//            Id = "0d133c1a-804f-4548-8f7e-8c3f504844e0",
//            DateOfBirth = DateOnly.Parse("2000/12/30"),
//            DepartmentId=1,
//            Email="agershaban7@gmail.com",
//            FirstName="hager",
//            LastName="shaban",
//            Gender="female",
//            PhoneNumber = "0621654984"
            
//        },
//        new Employee
//        {
//            Id = "sOB316984165",
//            DateOfBirth = DateOnly.Parse("2000/12/30"),
//            DepartmentId = 1,
//            Email = "agershaban7@gmail.com",
//            FirstName = "hager",
//            LastName = "shaban",
//            Gender = "female",
//            PhoneNumber = "0621654984"

//        }
//        );
//    }
//}
