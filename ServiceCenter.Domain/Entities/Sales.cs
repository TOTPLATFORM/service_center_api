using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Sales : Employee
{
   public virtual Center Center { get; set; }
   public virtual ICollection<Report?>Reports { get; set; } = new HashSet<Report?>();
}

