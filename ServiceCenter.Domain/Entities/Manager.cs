using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Manager : ApplicationUser
{
    public string Responsibilities { get; set; } = "";
    public DateOnly HiringDate { get; set; }
    public int WorkingHours { get; set; }
    public int Experience { get; set; }
    public int DepartmentId { get; set; }
    public virtual Department Department { get; set; } = default;
    public virtual ICollection<TimeSlot> TimeSlots { get; set; } = new HashSet<TimeSlot>();

}
