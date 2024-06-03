using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class TransactionMapping
{
   
        public static void AddTransactionMapping(this MappingProfiles map)
        {
            map.CreateMap<TransactionRequestDto, Transaction>()
               .ForMember(dest => dest.Inventory.Id, src => src.MapFrom(src => src.InventoryId))
               .ForMember(dest => dest.Vendor.Id, src => src.MapFrom(src => src.VendorId))
               .ReverseMap();
            map.CreateMap<Transaction, TransactionResponseDto>()
               .ReverseMap();
        }
    

}
