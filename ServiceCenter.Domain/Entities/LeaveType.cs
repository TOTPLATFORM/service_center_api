using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class LeaveType : AuditableEntity
{
    public string TypeName { get; set; } = "";
    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }
}