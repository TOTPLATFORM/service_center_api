using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Applicant : ApplicationUser
{
    public DateTime ApplicationDate { get; set; }
    public int DepartmentId { get; set; }
    public virtual Department Department { get; set; }

}
