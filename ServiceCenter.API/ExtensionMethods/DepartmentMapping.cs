using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class DepartmentMapping
{
	public static void AddDepartmentMapping(this MappingProfiles map)
	{
		map.CreateMap<DepartmentRequestDto, Department>()
			.ReverseMap();

		map.CreateMap<Department, DepartmentResponseDto>()
			//.ForMember(dest=>dest.ca,src=>src.MapFrom(src=>src.))
			.ReverseMap();
	}
}
