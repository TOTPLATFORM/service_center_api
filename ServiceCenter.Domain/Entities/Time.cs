using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Time   : AuditableEntity
{ 
	public TimeOnly StartTime { get; set; }
	public TimeOnly EndTime { get; set; }
}
