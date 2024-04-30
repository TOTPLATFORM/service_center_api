using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods
{
    public static class ProductCategoryMapping
    {
        public static void AddProductCategoryMapping(this MappingProfiles map)
        {
            map.CreateMap<ProductCategoryRequestDto, ProductCategory>().ReverseMap();
            map.CreateMap<ProductCategory, ProductCategoryResponseDto>().ReverseMap();
        }
    }
}
