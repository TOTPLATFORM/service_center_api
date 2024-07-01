using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
namespace ServiceCenter.API.ExtensionMethods;

public static class ServiceProviderMapping
{
    public static void AddServiceProviderMapping(this MappingProfiles map)
    {
        map.CreateMap<ServiceProviderRequestDto,Domain.Entities.ServiceProvider>();

        map.CreateMap<Domain.Entities.ServiceProvider, ServiceProviderResponseDto>()
            .ForMember(dest => dest.ServiceProviderPhoneNumber, src => src.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.ServiceProviderEmail, src => src.MapFrom(src => src.Email));

    }
}
