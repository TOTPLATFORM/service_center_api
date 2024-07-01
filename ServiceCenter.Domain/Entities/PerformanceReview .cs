using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class PerformanceReview : AuditableEntity
{
    public DateOnly ReviewDate { get; set; }
    public decimal PerformanceRating { get; set; }
    public string PerformanceDetails { get; set; } = "";
    public string Comments { get; set; } = "";
    public string EmployeeId { get; set; } = "";
    public virtual Employee Employee { get; set; }
}