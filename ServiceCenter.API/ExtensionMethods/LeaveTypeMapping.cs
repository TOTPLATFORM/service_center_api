using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class LeaveTypeMapping
{
    public static void AddLeaveTypetMapping(this MappingProfiles map)
    {
        map.CreateMap<LeaveTypeRequestDto, LeaveType>().ReverseMap();
        map.CreateMap<LeaveType, LeaveTypeResponseDto>().ReverseMap();
    }
}
