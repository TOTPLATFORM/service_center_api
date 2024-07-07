using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class SalesMapping
{
    public static void AddSalesMapping(this MappingProfiles map)
    {
        map.CreateMap<SalesRequestDto, Sales>();

        map.CreateMap<Sales, SalesResponseDto>()
            .ForMember(dest => dest.DepartmentName, src => src.MapFrom(src => src.Department.DepartmentName));
        map.CreateMap<Sales, SalesGetByIdResponseDto>()
            .ForMember(dest => dest.DepartmentName, src => src.MapFrom(src => src.Department.DepartmentName))
           .ForMember(dest => dest.Country, src => src.MapFrom(src => src.Address.Country))
           .ForMember(dest => dest.City, src => src.MapFrom(src => src.Address.City))
           .ForMember(dest => dest.PostalCode, src => src.MapFrom(src => src.Address.PostalCode)); 

    }
}
