using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class CenterMapping
{
	public static void AddCenterMapping(this MappingProfiles map)
	{
		map.CreateMap<CenterRequestDto, Center>()
			.ReverseMap();

		map.CreateMap<Center, CenterResponseDto>()
			.ReverseMap();
	}
}
