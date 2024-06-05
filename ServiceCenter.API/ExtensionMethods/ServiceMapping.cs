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
             //  .ForPath(dest=>dest.ServiceProviderName,src=>src.MapFrom(src=>src.ServiceProviders.SelectMany(src=>src.FirstName + " " + src.LastName)))
               .ReverseMap();


    }
}
