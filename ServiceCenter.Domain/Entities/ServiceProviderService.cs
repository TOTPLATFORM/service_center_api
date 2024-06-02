using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class ServiceProviderService
{
    public string ServiceProviderId { get; set; } = default;
    public virtual ServiceProvider ServiceProvider { get; set; }

    public int ServiceId { get; set; }
    public virtual Service Service { get; set; }
}
