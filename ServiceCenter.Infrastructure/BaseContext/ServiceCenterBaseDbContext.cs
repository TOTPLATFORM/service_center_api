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
	public DbSet<Appotiment> Appotiments { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<ProductCategory> ProductCategories { get; set; }
	public DbSet<Branch> Branches { get; set; }
	public DbSet<Center> Centers { get; set; }
	public DbSet<Room> Rooms { get; set; }
	public DbSet<Complaint> Complaints { get; set; }
	public DbSet<Customer> Customers { get; set; }
	public DbSet<Feedback> Feedbacks { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<Item> Items { get; set; }
	public DbSet<ItemCategory> ItemCategories { get; set; }
	public DbSet<Inventory> Inventories { get; set; }
	public DbSet<ProductBrand> ProductBrands { get; set; }
	public DbSet<Rating> Ratings { get; set; }
	public DbSet<Sales> Sales { get; set; }
	public DbSet<Vendor> Vendors { get; set; }
	public DbSet<Service> Services { get; set; }
	public DbSet<ServiceCategory> ServiceCategories { get; set; }
	public DbSet<ServicePackage> ServicePackages { get; set; }
	public DbSet<Contract> Contracts { get; set; }
	public DbSet<Department> Departments { get; set; }
	public DbSet<Employee> Employees { get; set; }
	public DbSet<WareHouseManager> WareHouseManagers { get; set; }
	public DbSet<Manager> Managers { get; set; }
	public DbSet<Offer> Offers { get; set; }
    public DbSet<CenterService>  CenterServices { get; set; }
	public DbSet<CustomerService> CustomerServices { get; set; }
	public DbSet<CustomerOffer> CustomerOffers { get; set; }
	public DbSet<RatingCustomer> RatingCustomers { get; set; }
	public DbSet<RatingService> RatingServices { get; set; }

}
