using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static  class FeedbackMapping
{
    public static void AddFeedbackMapping(this MappingProfiles map)
    {
        //map.CreateMap<FeedbackRequestDto, Feedback>()
        //     .ForMember(dest => dest.Contact.Id, src => src.MapFrom(src => src.ContactId))
        //      .ForMember(dest => dest.Service.Id, src => src.MapFrom(src => src.ServiceId))
        //       .ForMember(dest => dest.Product.Id, src => src.MapFrom(src => src.ProductId))
        //     .ReverseMap();
        map.CreateMap<Feedback, FeedbackResponseDto>()
                .ForMember(dest => dest.ContactName, src => src.MapFrom(src => src.Contact.FirstName))
            .ReverseMap();
    }
}
