using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class CustomerMapping
{
    public static void AddCustomerMapping(this MappingProfiles map)
    {
        map.CreateMap<CustomerRequestDto, Customer>();

        map.CreateMap<Customer, CustomerResponseDto>();

    }
}
