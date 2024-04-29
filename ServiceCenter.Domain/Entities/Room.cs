using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Room : AuditableEntity
{
	public int RoomNumber { get; set; }
	public bool Availability { get; set; }
    public int CenterId { get; set; }
    public virtual Center Center { get; set; }
}
