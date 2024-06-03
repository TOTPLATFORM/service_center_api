//using ServiceCenter.API.Mapping;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Domain.Entities;

//namespace ServiceCenter.API.ExtensionMethods;

//public static class VendorMapping
//{
//    public static void AddVendorMapping(this MappingProfiles map)
//    {
//        map.CreateMap<VendorRequestDto, Vendor>()
//            .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.VendorFirstName))
//            .ForMember(dest => dest.LastName, src => src.MapFrom(src => src.VendorLastName))
//            .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.VendorPhoneNumber))
//            .ForMember(dest => dest.Email, src => src.MapFrom(src => src.VendorEmail))
//            .ReverseMap();

//        map.CreateMap<Vendor, VendorResponseDto>()
//            .ForMember(dest => dest.VendorFirstName, src => src.MapFrom(src => src.FirstName))
//            .ForMember(dest => dest.VendorLastName, src => src.MapFrom(src => src.LastName))
//            .ForMember(dest => dest.VendorPhoneNumber, src => src.MapFrom(src => src.PhoneNumber))
//            .ForMember(dest => dest.VendorEmail, src => src.MapFrom(src => src.Email))
//            .ReverseMap();
//    }
//}
