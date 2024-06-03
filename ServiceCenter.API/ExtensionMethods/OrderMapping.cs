//using ServiceCenter.API.Mapping;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Domain.Entities;

//namespace ServiceCenter.API.ExtensionMethods;

//public static class OrderMapping
//{
//    public static void AddOrderMapping(this MappingProfiles map)
//    {
//        map.CreateMap<OrderRequestDto, Order>()
//            .ReverseMap();

//        map.CreateMap<Order, OrderResponseDto>()
//            .ReverseMap();

//        map.CreateMap<ItemOrderRequestDto, ItemOrder>()
//            .ReverseMap();

//        map.CreateMap<ItemOrder, ItemOrderResponseDto>()    
//            .ReverseMap();

//        map.CreateMap<ItemCategory,ItemCategoryResponseDto>()
//            .ForMember(dest=>dest.InventoryName,src=>src.MapFrom(src=>src.Inventory.InventoryName)) 
//            .ReverseMap();

//    }
//}
