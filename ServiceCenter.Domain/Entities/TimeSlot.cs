using ServiceCenter.Core.Entities;

namespace ServiceCenter.Domain.Entities;

public class TimeSlot : AuditableEntity 
{
	public string Day { get; set; } = "";
    public int ManagerId { get; set; }
    public Manager Manager { get; set; }
}