using ServiceCenter.Core.Entities;

namespace ServiceCenter.Domain.Entities;

public class TimeSlot : Time 
{
	public string Day { get; set; } = "";
    public string? ManagerId { get; set; }
    public virtual Manager Manager { get; set; }
}