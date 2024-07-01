using ServiceCenter.Core.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class LeaveRequest : AuditableEntity
{
    public int Id { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string Description { get; set; } = "";
    public Status Status { get; set; } = Status.Approved;
    public string EmployeeId { get; set; } = "";
    public int LeaveTypeId { get; set; }
    public virtual Employee Employee { get; set; }
    public virtual LeaveType LeaveType { get; set; }

}