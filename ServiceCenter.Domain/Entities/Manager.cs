using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Manager:ApplicationUser
{
    public virtual Branch Branch { get; set; }
    public virtual ICollection<Report?> Reports { get; set; } = new HashSet<Report>();
    public virtual ICollection<Campagin?> Campagins { get; set; } = new HashSet<Campagin>();
   // public virtual ICollection<Contact?> Contact { get; set; } = new HashSet<Contact>();
}
