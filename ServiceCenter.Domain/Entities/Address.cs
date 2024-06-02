using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

[Owned]
public class Address 
{
	public City City { get; set; } 
    public Country Country { get; set; } = Country.Egypt;
	public string PostalCode { get; set; } = "";


}
