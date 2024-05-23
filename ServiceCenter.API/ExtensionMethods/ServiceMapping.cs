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
           .ForMember(dest => dest.EmployeeName, src => src.MapFrom(src => src.Employee.FirstName))
            .ForMember(dest => dest.EmployeeId, src => src.MapFrom(src => src.Employee.Id))
             .ForMember(dest => dest.ServiceCategoryId, src => src.MapFrom(src => src.ServiceCategory.Id))
           .ReverseMap();

        map.CreateMap<Service,ServiceGetByIdResponseDto>()
            .ForMember(dest => dest.ServiceCategoryName, src => src.MapFrom(src => src.ServiceCategory.ServiceCategoryName))
            .ReverseMap();
    }
}
