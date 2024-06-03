//using ServiceCenter.API.Mapping;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Domain.Entities;

//namespace ServiceCenter.API.ExtensionMethods;

//public static class WareHouseManagerMapping
//{
//    public static void AddWareHouseManagerMapping(this MappingProfiles map)
//    {
//        map.CreateMap<WareHouseManagerRequestDto, WareHouseManager>()
//            .ForMember(d => d.Email, o => o.MapFrom(s => s.WareHouseManagerEmail))
//            .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.WareHouseManagerPhoneNumber))
//            .ForMember(d => d.FirstName, o => o.MapFrom(s => s.WareHouseManagerFirstName))
//            .ForMember(d => d.LastName, o => o.MapFrom(s => s.WareHouseManagerLastName));
//        map.CreateMap<WareHouseManager, WareHouseManagerResponseDto>()
//            .ForMember(d => d.WareHouseManagerEmail, o => o.MapFrom(s => s.Email))
//            .ForMember(d => d.WareHouseManagerPhoneNumber, o => o.MapFrom(s => s.PhoneNumber))
//            .ForMember(d => d.WareHouseManagerFirstName, o => o.MapFrom(s => s.FirstName))
//            .ForMember(d => d.WareHouseManagerLastName, o => o.MapFrom(s => s.LastName))
//            .ForMember(d => d.InventoryName, o => o.MapFrom(s => s.Inventory.InventoryName));
//    }
//}
