using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class ScheduleMapping
{
    public static void AddScheduleMapping(this MappingProfiles map)
    {
        map.CreateMap<ScheduleRequestDto, Schedule>().ReverseMap();
        map.CreateMap<Schedule, ScheduleResponseDto>()
            .ForMember(dest => dest.ServiceName, src => src.MapFrom(src => src.Service.ServiceName));
    }
}
