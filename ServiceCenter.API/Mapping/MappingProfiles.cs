using AutoMapper;
using ServiceCenter.API.ExtensionMethods;
using System.Security.Claims;

namespace ServiceCenter.API.Mapping;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		this.AddProductBrandMapping();

		this.AddInventoryMapping();

		this.AddItemMapping();

		this.AddOrderMapping();

		this.AddItemCategoryMapping();

		this.AddServiceCategoryMapping();

		this.AddCenterMapping();

		this.AddProductCategoryMapping();

		this.AddProductMapping();

		this.AddOrderMapping();

		this.AddEmployeeMapping();

		this.AddBranchMapping();

		this.AddDepartmentMapping();

		this.AddFeedbackMapping();

		this.AddCompliantMapping();

		this.AddServicePackageMapping();

		 this.AddScheduleMapping();

		this.AddServiceMapping();

		this.AddSubscriptionMapping();

		this.AddOfferMapping();

		this.AddAppointmentMapping();

	    this.AddContactMapping();

		this.AddReportMapping();

		this.AddAuthMapping();

		this.AddRatingServiceMapping();

        this.AddVendorMapping();

		this.AddWareHouseManagerMapping();

        this.AddManagerMapping();
        this.AddCampaginMapping();

		this.AddSalesMapping();

		this.AddCampaginMapping();

		this.AddServiceProviderMapping();
		this.AddExpenseMapping();
		this.AddRevenueMapping();
		this.AddRecruitmentMapping();
		this.AddPreformanceMapping();
		this.AddAttendanceMapping();
		this.AddSalaryMapping();
		this.AddApplicantMapping();
		this.AddLeaveRequestMapping();
		this.AddLeaveTypetMapping();
		this.AddCustomerMapping();
		this.AddProductOrderMapping();
		this.AddUserMapping();
	}

}
