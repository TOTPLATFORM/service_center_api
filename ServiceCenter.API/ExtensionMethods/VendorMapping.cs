using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class VendorMapping
{
    public static void AddVendorMapping(this MappingProfiles map)
    {
        map.CreateMap<VendorRequestDto, Vendor>();

        map.CreateMap<Vendor, VendorResponseDto>();
        map.CreateMap<Vendor,VendorGetByIdResponseDto>()
            .ForMember(dest=>dest.Country,src=>src.MapFrom(src=>src.Address.Country))
            .ForMember(dest=>dest.City,src=>src.MapFrom(src=>src.Address.City))
            .ForMember(dest=>dest.PostalCode,src=>src.MapFrom(src=>src.Address.PostalCode));
    }
}
