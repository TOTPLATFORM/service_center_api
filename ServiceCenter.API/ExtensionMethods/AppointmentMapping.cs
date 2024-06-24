using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class AppointmentMapping
{
    public static void AddAppointmentMapping(this MappingProfiles map)
    {
        map.CreateMap<AppointmentRequestDto, Appointment>();
        map.CreateMap<Appointment, AppointmentResponseDto>()
            .ForMember(dest => dest.Service, src => src.MapFrom(src => src.Schedule.Service))
            .ForMember(dest => dest.StartTime, src => src.MapFrom(src => src.Schedule.StartTime))
            .ForMember(dest => dest.EndTime, src => src.MapFrom(src => src.Schedule.EndTime));
    }
}

