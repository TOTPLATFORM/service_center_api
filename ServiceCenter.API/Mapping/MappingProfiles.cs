using AutoMapper;
using ServiceCenter.API.ExtensionMethods;
using System.Security.Claims;

namespace ServiceCenter.API.Mapping;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        this.AddTimeSlotMapping();

        this.AddProductBrandMapping();

        this.AddInventoryMapping();

      
    }
}
