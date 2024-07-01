using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class ExpensesTest
{
    public static void AddExpenses(this ServiceCenterBaseDbContext context)
    {
        context.Expenses.AddRange(
            new Expense
            {
                Id = 1,
                Value = 1,
                CreatedBy = "hager",
                CreatedDate = DateTime.Now,
                ModifiedBy = "hager",
                UpdatedDate = DateTime.Now
            },
            new Expense
            {
                Id = 2,
                Value = 3,
                CreatedBy = "hager",
                CreatedDate = DateTime.Now,
                ModifiedBy = "hager",
                UpdatedDate = DateTime.Now
            },
            new Expense
            {
                Id = 3,
                Value = 10,
                CreatedBy = "hager",
                CreatedDate = DateTime.Now,
                ModifiedBy = "hager",
                UpdatedDate = DateTime.Now
            }
        );
    }
}