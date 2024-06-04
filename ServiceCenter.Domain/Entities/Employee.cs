namespace ServiceCenter.Domain.Entities;

public class Employee : ApplicationUser
{
    public virtual Department Department { get; set; } = default;
}