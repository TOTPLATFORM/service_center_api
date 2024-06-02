using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class ServiceProvider:Employee
{
    public virtual ICollection<Service> Services { get; set; }
    public virtual ICollection<Schedule> Schedules { get; set; }
}
