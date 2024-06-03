//using ServiceCenter.API.Mapping;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Domain.Entities;

//namespace ServiceCenter.API.ExtensionMethods;

//public static class AppointmentMapping
//{
//    public static void AddAppointmentMapping(this MappingProfiles map)
//    {
//        map.CreateMap<AppointmentRequestDto, Appointment>();
//        map.CreateMap<Appointment, AppointmentResponseDto>()
//            .ForMember(d => d.StartTime, o => o.MapFrom(s => s.Schedule.TimeSlot.StartTime))
//            .ForMember(d => d.EndTime, o => o.MapFrom(s => s.Schedule.TimeSlot.EndTime))
//            .ForMember(d => d.Day, o => o.MapFrom(s => s.Schedule.TimeSlot.Day))
//            .ForMember(d => d.Employee, o => o.MapFrom(s => s.Schedule.Employee));
//    }
//}

