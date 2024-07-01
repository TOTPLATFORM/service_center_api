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
            .ReverseMap();
    }
}
