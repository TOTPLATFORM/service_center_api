using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Manager:Employee
{
	public string Responsibilities { get; set; } = "";
	public DateOnly HiringDate { get; set; }
	public int WorkingHours { get; set; }
	public int Experience { get; set; }
    public virtual ICollection<Report?> Reports { get; set; } = new HashSet<Report>();
    public virtual ICollection<Campagin?> Campagins { get; set; } = new HashSet<Campagin>();
    public virtual  Branch Branch { get; set; } = default;
    public int BranchId { get; set; }
}
