using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class AppointmentMapping
{
    public static void AddAppointmentMapping(this MappingProfiles map)
    {
        map.CreateMap<AppointmentRequestDto, Appointment>().ReverseMap();
        map.CreateMap<Appointment, AppointmentResponseDto>()
           .ForMember(dest => dest.CustomerName, src => src.MapFrom(src => src.Customer.FirstName))
            .ReverseMap();
    }
}

