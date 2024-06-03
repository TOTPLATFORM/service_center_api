using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static  class ServiceMapping
{
    public static void AddServiceMapping(this MappingProfiles map)
    {
        map.CreateMap<ServiceRequestDto, Service>()
			   .ReverseMap();

        map.CreateMap<Service, ServiceResponseDto>()
		   .ForMember(dest => dest.ServiceCategoryName, src => src.MapFrom(src => src.ServiceCategory.ServiceCategoryName))
           .ReverseMap();

        map.CreateMap<Service,ServiceGetByIdResponseDto>()
            .ForMember(dest => dest.ServiceCategoryName, src => src.MapFrom(src => src.ServiceCategory.ServiceCategoryName))
            .ReverseMap();
    }
}
