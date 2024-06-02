namespace ServiceCenter.Domain.Entities;

public class Employee :ApplicationUser
{
    public int DepartmentId { get; set; }
    public virtual Department Department { get; set; } = default;