using ServiceCenter.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.BaseContext;

public class ServiceCenterBaseDbContext : IdentityDbContext<ApplicationUser>
{
    public ServiceCenterBaseDbContext(DbContextOptions dbContext) : base(dbContext)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    public DbSet<TimeSlot> TimeSlots { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<ItemCategory> ItemCategories { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Order> Order { get; set; }


}
