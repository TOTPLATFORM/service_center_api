using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Manager:Employee
{

    public virtual ICollection<Report?> Reports { get; set; } = new HashSet<Report>();
    public virtual ICollection<Campagin?> Campagins { get; set; } = new HashSet<Campagin>();
}
