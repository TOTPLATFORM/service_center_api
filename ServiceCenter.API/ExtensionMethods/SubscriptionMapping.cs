using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class SubscriptionMapping
{
    public static void AddSubscriptionMapping(this MappingProfiles map)
    {
        map.CreateMap<SubscriptionRequestDto, Subscription>()
            .ForPath(dest => dest.Customer.Contact.Id, src => src.MapFrom(src => src.CustomerId))
            .ReverseMap();
        map.CreateMap<Subscription, SubscriptionResponseDto>()
            .ReverseMap();
    }
}

