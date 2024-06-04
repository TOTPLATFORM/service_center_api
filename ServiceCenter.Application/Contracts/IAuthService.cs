using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace ServiceCenter.Application.Contracts;

public interface IAuthService: IApplicationService, IScopedService
{
	/// <summary>
	/// logs a user in.
	/// </summary>
	/// <param name="LoginRequestDto">the login model.</param>
	/// <returns>the result of the login attempt.</returns>
	Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto LoginRequestDto);

	/// <summary>
	/// registers a new employee with employee role.
	/// </summary>
	/// <param name="employeeRequestDto">the Dto of the employee.</param>
	/// <returns>the result of the registration attempt.</returns>
	Task<Result> RegisterEmployeeAsync(EmployeeRequestDto employeeRequestDto);

	/// <summary>
	/// registers a new customer with customer role.
	/// </summary>
	/// <param name="customerRequestDto">the Dto of the customer.</param>
	/// <returns>the result of the registration attempt.</returns>
	//Task<Result> RegisterCustomerAsync(CustomerRequestDto customerRequestDto);

	/// <summary>
	/// registers a new warehouse manager with warehouse manager role.
	/// </summary>
	/// <param name="wareHouseManagerRequestDto">the Dto of the warehouse manager.</param>
	/// <returns>the result of the registration attempt.</returns>
	Task<Result> RegisterWarehouseManagerAsync(WareHouseManagerRequestDto wareHouseManagerRequestDto);

	/// <summary>
	/// registers a new sales  with sales role.
	/// </summary>
	/// <param name="salesRequestDto">the Dto of the sales.</param>
	/// <returns>the result of the registration attempt.</returns>
	Task<Result> RegisterSalesAsync(SalesRequestDto salesRequestDto);

	/// <summary>
	/// registers a new vendor  with vendor role.
	/// </summary>
	/// <param name="salesRequestDto">the Dto of the vendor.</param>
	/// <returns>the result of the registration attempt.</returns>
	Task<Result> RegisterVendorAsync(VendorRequestDto vendorRequestDto);

	/// <summary>
	/// adds a user to specific role.
	/// </summary>
	/// <param name="userId">the user ID.</param>
	/// <param name="roleName">the role name.</param>
	/// <returns>the result of trying to add a user to role.</returns>
	Task<Result> AddUserToRoleAsync(string userId, string roleName);
    Task InitializeRoles();
    Task CreateManagerAccount();
	Task CreateAdminAccount();
}