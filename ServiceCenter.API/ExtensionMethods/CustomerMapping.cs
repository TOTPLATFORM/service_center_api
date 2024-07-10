﻿using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class CustomerMapping
{
    public static void AddCustomerMapping(this MappingProfiles map)
    {
        map.CreateMap<CustomerRequestDto, Customer>()
            .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.PhoneNumber))
            .ForMember(d => d.UserName, o => o.MapFrom(s => s.UserName))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.Email));

        map.CreateMap<Customer, CustomerResponseDto>()
			.ForMember(dest => dest.Country, src => src.MapFrom(src => src.Address.Country))
			.ForMember(dest => dest.PostalCode, src => src.MapFrom(src => src.Address.PostalCode))
			.ForMember(dest => dest.City, src => src.MapFrom(src => src.Address.City));
    }
}
