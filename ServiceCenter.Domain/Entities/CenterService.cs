using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class CenterService : BaseEntity
{
    public int CenterId { get; set; }
    public int ServiceId { get; set; }
    public virtual Center Center { get; set; } = default;
    public virtual Service Service { get; set; } = default;
}
