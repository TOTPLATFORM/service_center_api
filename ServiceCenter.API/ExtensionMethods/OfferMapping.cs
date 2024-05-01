using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class OfferMapping
{
    public static void AddOfferMapping(this MappingProfiles map)
    {
        map.CreateMap<OfferRequestDto, Offer>().ReverseMap();
        map.CreateMap<Offer, OfferResponseDto>()
           .ForMember(dest => dest.ProductName, src => src.MapFrom(src => src.Product.ProductName))
            .ReverseMap();
    }
}
