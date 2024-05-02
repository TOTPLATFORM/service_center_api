﻿using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static  class ServiceMapping
{
    public static void AddServiceMapping(this MappingProfiles map)
    {
        map.CreateMap<ServiceCategoryRequestDto, Service>();
        map.CreateMap<List<ServiceRequestDto>, List<Service>>()
               .ReverseMap();

        map.CreateMap<Service, ServiceResponseDto>()
            .ForMember(dest => dest.LinkedPackages, src => src.MapFrom(src => src.ServicePackages))
             .ForMember(dest => dest.ServiceCategoryName, src => src.MapFrom(src => src.ServiceCategory.ServiceCategoryName))
              .ForMember(dest => dest.EmployeeName, src => src.MapFrom(src => src.Employee.FirstName))
            .ReverseMap();
    }
}
