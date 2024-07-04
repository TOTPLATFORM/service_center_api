using ServiceCenter.Infrastructure.BaseContext;
using ServiceCenter.Test.TestSetup;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceCenter.Test.TestSetup.Data;

namespace ServiceCenter.Test.TestSetup;

public class ContextGenerator
{
    private static ServiceCenterBaseDbContext Context;
    public static ServiceCenterBaseDbContext Generator()
    {
        if (Context == null)
        {
            var options = new DbContextOptionsBuilder<ServiceCenterBaseDbContext>()
           .UseInMemoryDatabase(databaseName: "ServiceCenterSystem")
           .Options;
            Context = new ServiceCenterBaseDbContext(options);
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
            Context.AddItem();
            Context.AddItemCategory();
            Context.AddInventory();
            Context.AddDepartment();
            Context.AddService();
            Context.AddFeedback();
            Context.AddComplaint();
            Context.AddCenter();
            Context.AddBranch();
            Context.AddContact();
            Context.AddSubscription();
            Context.AddOffer();
            Context.AddSchedule();
            Context.AddServicePackage();
            Context.AddAppointment();
            Context.AddProductBrand();
            Context.AddRating();
            Context.AddCampagin();
            Context.AddProductCategory();
            Context.AddServiceCategory();
            Context.AddSales();
            Context.AddManager();
            Context.AddWareHouseManager();
            Context.AddServiceProvider();   
            Context.AddVendor();
            Context.AddReport();
            Context.AddRevenue();
            Context.AddExpenses();
            Context.AddSalary();
            Context.AddPerformanceReview();
            Context.AddRecruitmentRecord();
            Context.AddApplicant();
            Context.AddAttendance();
            Context.AddLeaveRequest();
            Context.AddLeaveType();
            Context.AddCustomer();
            Context.SaveChanges();
        }

        return Context;
    }
}
