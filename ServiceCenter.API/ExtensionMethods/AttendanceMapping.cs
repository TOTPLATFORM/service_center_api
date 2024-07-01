using ServiceCenter.API.Mapping;
using ServiceCenter.Application;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class AttendanceMapping
{
    public static void AddAttendanceMapping(this MappingProfiles map)
    {
        map.CreateMap<AttendanceRequestDto, Attendance>();
        map.CreateMap<Attendance, AttendanceResponseDto>();

    }
}
