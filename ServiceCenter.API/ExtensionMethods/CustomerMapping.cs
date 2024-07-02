using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class CustomerMapping
{
    public static void AddCustomerMapping(this MappingProfiles map)
    {
        map.CreateMap<CustomerRequestDto, Customer>()
            .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.User.PhoneNumber))
            .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.UserName));

		map.CreateMap<Customer, CustomerResponseDto>();

    }
}
