//using ServiceCenter.API.Mapping;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Domain.Entities;

//namespace ServiceCenter.API.ExtensionMethods;

public static class EmployeeMapping
{
	public static void AddEmployeeMapping(this MappingProfiles map)
	{
		map.CreateMap<EmployeeRequestDto, Employee>()
			.ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.EmployeeFirstName))
			.ForMember(dest => dest.LastName, src => src.MapFrom(src => src.EmployeeLastName))
			.ForMember(dest=>dest.PhoneNumber,src=>src.MapFrom(src => src.EmployeePhoneNumber))	
			.ForMember(dest=>dest.Email,src=>src.MapFrom(src=>src.EmployeeEmail))
                .ForMember(dest => dest.Department.Id, src => src.MapFrom(src => src.DepartmentId))
            .ReverseMap();

//		map.CreateMap<Employee, EmployeeResponseDto>()
//			.ForMember(dest => dest.EmployeeFirstName, src => src.MapFrom(src => src.FirstName))
//			.ForMember(dest => dest.EmployeeLastName, src => src.MapFrom(src => src.LastName))
//			.ForMember(dest => dest.EmployeePhoneNumber, src => src.MapFrom(src => src.PhoneNumber))
//			.ForMember(dest => dest.EmployeeEmail, src => src.MapFrom(src => src.Email))
//			.ForMember(dest => dest.DepartmentName, src => src.MapFrom(src => src.Department.DepartmentName))
//			.ReverseMap();
//	}
//}
