using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class EmployeeMapping
{
	public static void AddEmployeeMapping(this MappingProfiles map)
	{
		map.CreateMap<EmployeeRequestDto, Employee>()
			.ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.EmployeeFirstName))
			.ForMember(dest => dest.LastName, src => src.MapFrom(src => src.EmployeeLastName))
			.ForMember(dest=>dest.PhoneNumber,src=>src.MapFrom(src => src.EmployeePhoneNumber))	
			.ForMember(dest=>dest.Email,src=>src.MapFrom(src=>src.EmployeeEmail))
			.ReverseMap();

		map.CreateMap<Employee, EmployeeResponseDto>()
			.ForPath(dest => dest.DepartmentName, src => src.MapFrom(src => src.Department.DepartmentName))
			.ReverseMap();
	}
}
