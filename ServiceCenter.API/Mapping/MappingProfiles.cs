using AutoMapper;
using ServiceCenter.API.ExtensionMethods;
using System.Security.Claims;

namespace ServiceCenter.API.Mapping;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
      

        //this.AddProductBrandMapping();

        //this.AddInventoryMapping();

        //this.AddItemMapping();

        //this.AddOrderMapping();

        //this.AddItemCategoryMapping();

        //this.AddServiceCategoryMapping();

        //this.AddCenterMapping();
        
        //this.AddProductCategoryMapping();

        this.AddProductMapping();

        //this.AddItemMapping();

        //this.AddOrderMapping();

        //this.AddEmployeeMapping();

        //this.AddItemCategoryMapping();

        this.AddBranchMapping();

        //this.AddDepartmentMapping();

        //this.AddFeedbackMapping();

        //this.AddCompliantMapping();

        //this.AddServicePackageMapping();

        //this.AddScheduleMapping();

        //this.AddServiceMapping();

        //this.AddSubscriptionMapping();

        //this.AddOfferMapping();

        //this.AddAppointmentMapping(); 

        //this.AddContactMapping();

       

        //this.AddCampaginMapping();

        //this.AddSalesMapping();

        //this.AddReportMapping();

        this.AddAuthMapping();

        //this.AddRatingServiceMapping();

        //this.AddVendorMapping();

        //this.AddWareHouseManagerMapping();

     
	}

}
