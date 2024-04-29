using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class TimeSlotMapping 
{
	public static void AddTimeSlotMapping(this MappingProfiles map)
	{
		map.CreateMap<TimeSlotRequestDto, TimeSlot>().ReverseMap();
		map.CreateMap<TimeSlot, TimeSlotResponseDto>().ReverseMap();
	}
}
