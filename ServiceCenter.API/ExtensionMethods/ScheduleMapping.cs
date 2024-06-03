//using ServiceCenter.API.Mapping;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Domain.Entities;

//namespace ServiceCenter.API.ExtensionMethods;

//public static class ScheduleMapping
//{
//    public static void AddScheduleMapping(this MappingProfiles map)
//    {
//        map.CreateMap<ScheduleRequestDto, Schedule>().ReverseMap();
//        map.CreateMap<Schedule, ScheduleResponseDto>()
//           .ForMember(dest => dest.EmployeeName, src => src.MapFrom(src => src.Employee.FirstName))
//           .ForMember(dest => dest.StartTime, src => src.MapFrom(src => src.TimeSlot.StartTime))
//           .ForMember(dest => dest.EndTime, src => src.MapFrom(src => src.TimeSlot.EndTime))
//           .ForMember(dest => dest.Day, src => src.MapFrom(src => src.TimeSlot.Day))
//            .ReverseMap();

//        map.CreateMap<Schedule, ScheduleGetByIdResponseDto>()
//            .ForMember(d => d.StartTime, o => o.MapFrom(s => s.TimeSlot.StartTime))
//            .ForMember(d => d.EndTime, o => o.MapFrom(s => s.TimeSlot.EndTime))
//            .ForMember(d => d.Day, o => o.MapFrom(s => s.TimeSlot.Day));
//    }
//}
