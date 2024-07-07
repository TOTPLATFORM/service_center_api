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
            .ForMember(dest => dest.DepartmentName, src => src.MapFrom(src => src.Department.DepartmentName));

    }
}
