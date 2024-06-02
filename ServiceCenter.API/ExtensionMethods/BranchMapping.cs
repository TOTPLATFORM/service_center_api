using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class BranchMapping
{
	public static void AddBranchMapping(this MappingProfiles map)
	{
		map.CreateMap<BranchRequestDto, Branch>()
            .ForMember(dest => dest.Manager.Id, src => src.MapFrom(src => src.ManagerId))
            .ForMember(dest => dest.Inventory.Id, src => src.MapFrom(src => src.InventoryId))
            .ReverseMap();

		map.CreateMap<Branch, BranchResponseDto>()
		    .ForMember(dest => dest.Country, src => src.MapFrom(src => src.Address.Country))
			.ForMember(dest => dest.PostalCode, src => src.MapFrom(src => src.Address.PostalCode))
			.ForMember(dest => dest.City, src => src.MapFrom(src => src.Address.City))
			.ReverseMap();
	}
}
