//using ServiceCenter.API.Mapping;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Domain.Entities;

//namespace ServiceCenter.API.ExtensionMethods;

public static class OfferMapping
{
    public static void AddOfferMapping(this MappingProfiles map)
    {
        map.CreateMap<OfferRequestDto, Offer>()
            .ForMember(dest => dest.Product.Id, src => src.MapFrom(src => src.ProductId))
             .ForMember(dest => dest.Service.Id, src => src.MapFrom(src => src.ServiceId))
            .ReverseMap();
        map.CreateMap<Offer, OfferResponseDto>()
           .ReverseMap();

   //     map.CreateMap<Offer, ProductResponseDto>()
   //         .ForMember(dest=>dest.ProductName,src=>src.MapFrom(src=>src.Product.ProductName))
   //         .ForMember(dest => dest.ProductPrice, src => src.MapFrom(src => src.Product.ProductPrice))
			//.ForMember(dest => dest.ProductDescription, src => src.MapFrom(src => src.Product.ProductDescription))
			//.ForMember(dest => dest.CategoryName, src => src.MapFrom(src => src.Product.ProductCategory.CategoryName))
			//.ForMember(dest => dest.ProductBrandName, src => src.MapFrom(src => src.Product.ProductBrand.BrandName))
			//.ForMember(dest => dest.SalesName, src => src.MapFrom(src => src.Product.Sales.FirstName + " " + src.Product.Sales.LastName));
    }
}
