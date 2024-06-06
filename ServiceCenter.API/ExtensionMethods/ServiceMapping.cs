using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class ServiceMapping
{
    public static void AddServiceMapping(this MappingProfiles map)
    {
        map.CreateMap<ServiceRequestDto, Service>()
            .ReverseMap();

        map.CreateMap<Service, ServiceResponseDto>()
            .ForMember(dest=>dest.Center.CenterName,src=>src.MapFrom(src=>src.Center.CenterName))
            .ReverseMap();


    }
}
