using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Appotiment: AuditableEntity
{
	public TimeOnly StartTime { get; set; }
	public TimeOnly EndTime { get; set; }
	public DateOnly Date { get; set; }
	public int CustomerId { get; set; }
    public Customer Customer { get; set; }



}
