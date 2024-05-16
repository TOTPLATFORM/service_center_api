using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class CampaginMapping
{
	public static void AddCampaginMapping(this MappingProfiles map)
	{
		map.CreateMap<CampaginRequestDto, Campagin>();

		map.CreateMap<Campagin, CampaginResponseDto>();
	}
}
