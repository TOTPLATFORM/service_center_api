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

          map.CreateMap<Service, ServiceGetByIdResponseDto>()
          .ForPath(dest => dest.ServiceProviderId,
                opt => opt.MapFrom(src => src.ServiceProviders.Select(sp => sp.Id).ToList()))
            .ReverseMap();
        map.CreateMap<ServiceResponseDto, Service>()
          .ReverseMap();


    }
}
