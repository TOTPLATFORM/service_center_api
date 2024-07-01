using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class VendorMapping
{
    public static void AddVendorMapping(this MappingProfiles map)
    {
        map.CreateMap<VendorRequestDto, Vendor>()
            .ReverseMap();

        map.CreateMap<Vendor, VendorResponseDto>()
            .ForMember(dest => dest.VendorPhoneNumber, src => src.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.VendorEmail, src => src.MapFrom(src => src.Email))
            .ReverseMap();
    }
}
