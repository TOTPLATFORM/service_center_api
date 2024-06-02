using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class ContractMapping
{
    public static void AddContractMapping(this MappingProfiles map)
    {
        map.CreateMap<ContractRequestDto, Subscription>().ReverseMap();
        map.CreateMap<Subscription, ContractResponseDto>()
           .ForMember(dest => dest.PackageName, src => src.MapFrom(src => src.ServicePackage.PackageName))
            .ReverseMap();
    }
}

