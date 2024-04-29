﻿using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Appotiment: Time
{
	
	public DateOnly Date { get; set; }
	public int CustomerId { get; set; }
    public Customer Customer { get; set; }
	public int EmployeeId { get; set; }
	public Employee Employee { get; set; }



}
