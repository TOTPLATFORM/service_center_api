using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class EmployeeMapping
{
	public static void AddEmployeeMapping(this MappingProfiles map)
	{
		map.CreateMap<EmployeeRequestDto, Employee>();

        map.CreateMap<Employee, EmployeeGetByIdResponseDto>()
            .ForMember(dest => dest.DepartmentName, src => src.MapFrom(src => src.Department.DepartmentName));

        map.CreateMap<Employee, EmployeeResponseDto>()
            .ForMember(dest => dest.DepartmentName, src => src.MapFrom(src => src.Department.DepartmentName));
    }
}
