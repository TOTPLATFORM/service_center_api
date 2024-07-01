using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public  static class SalaryMapping
{
    public static void AddSalaryMapping(this MappingProfiles map)
    {
        map.CreateMap<SalaryRequestDto, Salary>();
        map.CreateMap<SalaryUpdateDto, Salary>();
        map.CreateMap<Salary, SalaryResponseDto>();
    }
}
