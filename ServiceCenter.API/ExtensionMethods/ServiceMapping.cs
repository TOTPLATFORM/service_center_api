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

          map.CreateMap< ServiceResponseDto,Service>()
         // .ForMember(dest => dest.ServiceProviders.Select(s => s.Id), src => src.MapFrom(src => src.ServiceProviderId))

            .ReverseMap();


    }
}
