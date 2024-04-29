namespace ServiceCenter.Domain.Entities;

public class Employee : ApplicationUser
{
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    public ICollection<Service> Services { get; set; }  =  new HashSet<Service>();
}