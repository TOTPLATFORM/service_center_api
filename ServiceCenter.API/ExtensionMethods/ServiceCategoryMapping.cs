using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class ServiceCategoryMapping
{
    public static void AddServiceCategoryMapping(this MappingProfiles map)
    {
        map.CreateMap<ServiceCategoryRequestDto, ServiceCategory>().ReverseMap();
        map.CreateMap<ServiceCategory, ServiceCategoryResponseDto>().ReverseMap();
    }
}
