using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class RatingServiceMapping
{
    public static void AddRatingServiceMapping(this MappingProfiles map)
    {
        map.CreateMap<RatingServiceRequestDto, RatingService>();
        map.CreateMap<RatingService, RatingServiceResponseDto>()
            .ForMember(d => d.CustomerName, o => o.MapFrom(s => s.Customer.FirstName + " " + s.Customer.LastName))
            .ForMember(d => d.ServiceName, o => o.MapFrom(s => s.Service.ServiceName));
        map.CreateMap<RatingService, RatingServiceGetByIdResponseDto>();
    }
}
