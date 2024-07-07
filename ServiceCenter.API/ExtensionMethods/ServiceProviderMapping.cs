using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;
using ServiceProvider = ServiceCenter.Domain.Entities.ServiceProvider;
namespace ServiceCenter.API.ExtensionMethods;

public static class ServiceProviderMapping
{
    public static void AddServiceProviderMapping(this MappingProfiles map)
    {
        map.CreateMap<ServiceProviderRequestDto,ServiceProvider>();

        map.CreateMap<ServiceProvider, ServiceProviderResponseDto>()
            .ForMember(dest => dest.DepartmentName, src => src.MapFrom(src => src.Department.DepartmentName));
        map.CreateMap<ServiceProvider, ServiceProviderGetByIdResponseDto>()
           .ForMember(dest => dest.DepartmentName, src => src.MapFrom(src => src.Department.DepartmentName));

    }
}
