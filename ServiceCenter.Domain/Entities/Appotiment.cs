using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Appointment : Time
{
	public DateOnly Date { get; set; }
	public string Day { get; set; } = "";
    public string CustomerId { get; set; } = default;
	public virtual Customer Customer { get; set; } = default;



}

