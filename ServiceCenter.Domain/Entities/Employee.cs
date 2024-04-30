namespace ServiceCenter.Domain.Entities;

public class Employee :ApplicationUser
{
    public int DepartmentId { get; set; }
    public virtual Department Department { get; set; } = default;
    public virtual ICollection<Service> Services { get; set; }  =  new HashSet<Service>();
}