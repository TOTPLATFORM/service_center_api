using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class UserMapping
{
    public static void AddUserMapping(this MappingProfiles map)
    {
        map.CreateMap<BaseUserRequestDto, ApplicationUser>();

        map.CreateMap<ApplicationUser, BaseUserResponseDto>()
            .ReverseMap();

    }
}
