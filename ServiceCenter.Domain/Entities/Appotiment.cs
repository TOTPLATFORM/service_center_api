using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Appotiment: Time
{
	
	public DateOnly Date { get; set; }
	public string CustomerId { get; set; } = "";
    public Customer Customer { get; set; }
	public string EmployeeId { get; set; } = "";
	public Employee Employee { get; set; }



}
