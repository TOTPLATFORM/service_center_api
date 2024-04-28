using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Center : AuditableEntity
{
	public string CenterName { get; set; } = "";
    public int OpeningHours { get; set; }
    public string Specialty { get; set; }
	public ICollection<Branch> branches { get; set; } = new HashSet<Branch>();
    public ICollection<Department> Departments { get; set; } = new HashSet<Department>();
    public ICollection<Room> Rooms { get; set; } = new HashSet<Room>();
}
