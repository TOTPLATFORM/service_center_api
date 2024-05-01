using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class RoomMapping
{
    public static void AddRoomMapping(this MappingProfiles map)
    {
        map.CreateMap<RoomRequestDto, Room>().ReverseMap();
        map.CreateMap<Room, RoomResponseDto>()
           .ForMember(dest => dest.CenterName, src => src.MapFrom(src => src.Center.CenterName))
            .ReverseMap();
    }
}
