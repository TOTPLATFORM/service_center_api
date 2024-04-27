using ServiceCenter.Infrastructure.BaseContext;
using ServiceCenter.Test.TestSetup;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSWithLayers.Test.TestSetup;

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
            
            Context.SaveChanges();
        }

        return Context;
    }
}
