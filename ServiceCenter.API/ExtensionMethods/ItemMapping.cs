using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static  class ItemMapping
{
    public static void AddItemMapping(this MappingProfiles map)
    {
        map.CreateMap< ItemRequestDto,  Item>().ReverseMap()
            .ForMember(d => d.CategoryId,o => o.MapFrom(s => s.Category.Id));
        map.CreateMap< Item,  ItemResponseDto>().ReverseMap();
    }
}
