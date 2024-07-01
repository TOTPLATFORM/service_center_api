using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class LeaveRequestMapping
{
    public static void AddLeaveRequestMapping(this MappingProfiles map)
    {
        map.CreateMap<LeaveRequestRequestDto, LeaveRequest>().ReverseMap();
        map.CreateMap<LeaveRequest, LeaveRequestResponseDto>().ReverseMap();
    }
}
