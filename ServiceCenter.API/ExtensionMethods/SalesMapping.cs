using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class SalesMapping
{
    public static void AddSalesMapping(this MappingProfiles map)
    {
        map.CreateMap<SalesRequestDto, Sales>()
  
            //.ForMember(dest => dest.Center.Id, src => src.MapFrom(src => src.CenterId))
            .ReverseMap();

        map.CreateMap<Sales, SalesResponseDto>()
            .ForMember(dest => dest.SalesFirstName, src => src.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.SalesLastName, src => src.MapFrom(src => src.LastName))
            .ForMember(dest => dest.SalesPhoneNumber, src => src.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.SalesEmail, src => src.MapFrom(src => src.Email))
            .ReverseMap();
    }
}
