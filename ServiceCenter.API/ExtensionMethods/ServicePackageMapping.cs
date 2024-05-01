using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class ServicePackageMapping
{
    public static void AddServicePackageMapping(this MappingProfiles map)
    {
        map.CreateMap<ServicePackageRequestDto, ServicePackage>().ReverseMap();
        map.CreateMap<ServicePackage, ServicePackageResponseDto>().ReverseMap();
    }
}
