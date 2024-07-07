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
            .ForPath(dest => dest.DepartmentName, src => src.MapFrom(src => src.Department.DepartmentName))
            .ForMember(d => d.InventoryName, o => o.MapFrom(s => s.Inventory.InventoryName));
        map.CreateMap<WareHouseManager, WareHouseManagerGetByIdResponseDto>()
           .ForMember(dest => dest.DepartmentName, src => src.MapFrom(src => src.Department.DepartmentName))
          .ForMember(d => d.InventoryName, o => o.MapFrom(s => s.Inventory.InventoryName));
    }
}
