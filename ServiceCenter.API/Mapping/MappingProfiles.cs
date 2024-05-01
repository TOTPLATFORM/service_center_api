﻿using AutoMapper;
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

        this.AddCenterMapping();
        
        this.AddProductCategoryMapping();

        this.AddProductMapping();

        this.AddItemMapping();

        this.AddOrderMapping();
        this.AddEmployeeMapping();

        this.AddItemCategoryMapping();

        this.AddBranchMapping();

        this.AddDepartmentMapping();

        this.AddFeedbackMapping();

        this.AddCompliantMapping();

        this.AddServicePackageMapping();
       this.AddScheduleMapping();
        this.AddServiceMapping();
	}

}
