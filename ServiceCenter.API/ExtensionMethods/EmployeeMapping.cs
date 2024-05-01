using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class EmployeeMapping
{
	public static void AddEmployeeMapping(this MappingProfiles map)
	{
		map.CreateMap<EmployeeRequestDto, Employee>()
			.ReverseMap();

		map.CreateMap<Employee, EmployeeResponseDto>()
			.ForPath(dest => dest.Department.DepartmentName, src => src.MapFrom(src => src.Department.DepartmentName))
			.ReverseMap();
	}
}
