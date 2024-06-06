using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class VendorMapping
{
    public static void AddVendorMapping(this MappingProfiles map)
    {
        map.CreateMap<VendorRequestDto, Vendor>()
            .ForPath(dest => dest.Center.Id, src => src.MapFrom(src => src.CenterId))
            .ReverseMap();

        map.CreateMap<Vendor, VendorResponseDto>()
            .ForMember(dest => dest.VendorFirstName, src => src.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.VendorLastName, src => src.MapFrom(src => src.LastName))
            .ForMember(dest => dest.VendorPhoneNumber, src => src.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.VendorEmail, src => src.MapFrom(src => src.Email))
            .ForMember(dest => dest.CenterId, src => src.MapFrom(src => src.Center.Id))
            .ReverseMap();
    }
}
