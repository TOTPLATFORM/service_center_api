using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class RatingMapping
{
    public static void AddRatingServiceMapping(this MappingProfiles map)
    {
         map.CreateMap<RatingRequestDto, Rating>()
            .ForPath(dest => dest.Customer.Id, src => src.MapFrom(src => src.CustomerId))            
            .ForPath(dest => dest.Service.Id, src => src.MapFrom(src => src.ServiceId))
            .ForPath(dest => dest.Product.Id, src => src.MapFrom(src => src.ProductId))
            .ReverseMap();


        map.CreateMap<Rating, RatingResponseDto>()
           .ForMember(dest => dest.CustomerName, src => src.MapFrom(src => src .Customer.Contact.FirstName))
           .ForMember(dest => dest.RatingDate, opt => opt.MapFrom(src => src.CreatedDate))
           .ReverseMap();

    }
}
