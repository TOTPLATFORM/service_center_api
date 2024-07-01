using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Salary : AuditableEntity
{
    public DateOnly SalaryDate { get; set; }
    public decimal? Bonus { get; set; }
    public decimal? Deduction { get; set; }
    public string EmployeeId { get; set; } = "";
    public virtual Employee Employee { get; set; }
}