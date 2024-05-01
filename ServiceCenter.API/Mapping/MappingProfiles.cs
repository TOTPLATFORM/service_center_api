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

        this.AddItemMapping();

        this.AddOrderMapping();

        this.AddItemCategoryMapping();
        this.AddServiceCategoryMapping();

<<<<<<<<< Temporary merge branch 1
        this.AddCenterMapping();


=========
      
        this.AddProductCategoryMapping();
        this.AddProductMapping();
      
    
>>>>>>>>> Temporary merge branch 2
=========

        this.AddItemMapping();

        this.AddOrderMapping();

        this.AddItemCategoryMapping();

        this.AddBranchMapping();

        this.AddDepartmentMapping();


	}
>>>>>>>>> Temporary merge branch 2
}
