using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static  class FeedbackMapping
{
    public static void AddFeedbackMapping(this MappingProfiles map)
    {
        map.CreateMap<FeedbackRequestDto, Feedback>()
           .ForPath(dest => dest.Customer.Contact.Id, src => src.MapFrom(src => src.CustomerId))
           .ForPath(dest => dest.Service.Id, src => src.MapFrom(src => src.ServiceId))     
           .ForPath(dest => dest.Product.Id, src => src.MapFrom(src => src.ProductId))
           .ReverseMap();
        map.CreateMap<Feedback, FeedbackResponseDto>()
           .ForMember(dest => dest.CustomerName, src => src.MapFrom(src => src.Customer.Contact.FirstName))
           .ForMember(dest => dest.FeedbackDate, opt => opt.MapFrom(src => src.CreatedDate))
           .ReverseMap();
    }
}
