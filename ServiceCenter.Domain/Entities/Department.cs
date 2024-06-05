using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Department : AuditableEntity
{
	public string DepartmentName { get; set; } = "";
    public virtual Center Center { get; set; }

    public virtual ICollection<Employee?> Employees { get; set; } = new HashSet<Employee?>();

}
