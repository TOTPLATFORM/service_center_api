using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class InventoryMapping
{
	public static void AddInventoryMapping(this MappingProfiles map)
	{
		map.CreateMap<InventoryRequestDto, Inventory>()
			.ReverseMap();

		map.CreateMap<Inventory, InventoryResponseDto>()
		   	.ReverseMap();
	}
}
