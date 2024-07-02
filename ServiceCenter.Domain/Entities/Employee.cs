using ServiceCenter.Domain.Enums;

namespace ServiceCenter.Domain.Entities;

public class Employee : ApplicationUser
{
	public string FirstName { get; set; } = "";
	public string LastName { get; set; } = "";
	public DateOnly DateOfBirth { get; set; }
	public Gender Gender { get; set; }
	public Address Address { get; set; } = default;
	public int DepartmentId { get; set; }
    public decimal BaseSalary { get; set; }
    public virtual Department Department { get; set; }
    public virtual ICollection<Salary> Salaries { get; set; }
    public virtual PerformanceReview Performance { get; set; }
}