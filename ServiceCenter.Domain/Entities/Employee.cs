namespace ServiceCenter.Domain.Entities;

public class Employee : ApplicationUser
{
    public virtual Contact Contact { get; set; }
    public int ContactId { get; set; }
    public int DepartmentId { get; set; }
    public decimal BaseSalary { get; set; }
    public virtual Department Department { get; set; }
    public virtual ICollection<Salary> Salaries { get; set; }
    public virtual PerformanceReview Performance { get; set; }
}