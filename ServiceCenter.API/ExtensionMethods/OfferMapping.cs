using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class OfferMapping
{
    public static void AddOfferMapping(this MappingProfiles map)
    {
        map.CreateMap<OfferRequestDto, Offer>()
            .ForPath(dest => dest.Product.Id, src => src.MapFrom(src => src.ProductId))
             .ForPath(dest => dest.Service.Id, src => src.MapFrom(src => src.ServiceId))
             .ForMember(dest => dest.OfferName, src => src.MapFrom(src => src.OfferName.ToString()))
            .ReverseMap();
        map.CreateMap<Offer, OfferResponseDto>()
            .ForMember(dest => dest.OfferName, src => src.MapFrom(src => src.OfferName.ToString()))
           .ReverseMap();

        //map.CreateMap<Offer, ProductGetByIdResponseDto>()
        //    .ForMember(dest => dest.ProductName, src => src.MapFrom(src => src.Product.ProductName))
        //    .ForMember(dest => dest.ProductPrice, src => src.MapFrom(src => src.Product.ProductPrice))
        //    .ForMember(dest => dest.ProductDescription, src => src.MapFrom(src => src.Product.ProductDescription));
        //map.CreateMap<Offer, ServiceGetByIdResponseDto>()
        //    .ForMember(dest => dest.ServiceName, src => src.MapFrom(src => src.Service.ServiceName))
        //    .ForMember(dest => dest.ServiceDescription, src => src.MapFrom(src => src.Service.ServiceDescription))
        //    .ForMember(dest => dest.ServicePrice, src => src.MapFrom(src => src.Service.ServicePrice));
    }
}
