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
            Context.AddRoom();
            Context.AddTimeSlot();
            Context.AddFeedback();
            Context.AddComplaint();
            Context.AddCenter();
            Context.AddBranch();
            Context.AddContact();
            Context.AddContract();
            Context.AddEmployee();
            Context.AddOffer();
            Context.AddSchedule();
            Context.AddCustomer();
            Context.AddAppointment();
            Context.AddProductBrand();
            Context.AddRatingService();
            Context.AddCampagin();
            Context.AddOverview();
            Context.AddProductCategory();
            Context.AddServiceCategory();
            Context.SaveChanges();
        }

        return Context;
    }
}
