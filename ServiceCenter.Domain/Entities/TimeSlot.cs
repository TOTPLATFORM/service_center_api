using ServiceCenter.Core.Entities;

namespace ServiceCenter.Domain.Entities;

public class TimeSlot : Time 
{
	public string Day { get; set; } = "";
}