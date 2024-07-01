using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class WareHouseManagerMapping
{
    public static void AddWareHouseManagerMapping(this MappingProfiles map)
    {
        map.CreateMap<WareHouseManagerRequestDto, WareHouseManager>();
        map.CreateMap<WareHouseManager, WareHouseManagerResponseDto>()
            .ForMember(d => d.WareHouseManagerEmail, o => o.MapFrom(s => s.Email))
            .ForMember(d => d.WareHouseManagerPhoneNumber, o => o.MapFrom(s => s.PhoneNumber))
            .ForMember(d => d.InventoryName, o => o.MapFrom(s => s.Inventory.InventoryName));
    }
}
