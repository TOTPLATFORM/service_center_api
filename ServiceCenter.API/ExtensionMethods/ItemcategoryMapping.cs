//using ServiceCenter.API.Mapping;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Domain.Entities;

//namespace ServiceCenter.API.ExtensionMethods;

//public static class ItemcategoryMapping
//{
//    public static void AddItemCategoryMapping(this MappingProfiles map)
//    {
//        map.CreateMap< ItemCategoryRequestDto,  ItemCategory>()
//            .ReverseMap();

        map.CreateMap< ItemCategory,  ItemCategoryResponseDto>()
            .ReverseMap();
    }
}
