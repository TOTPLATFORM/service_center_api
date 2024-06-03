//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using ServiceCenter.Application.Contracts;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Core.Result;

//namespace ServiceCenter.API.Controllers;

//public class DepartmentController(IDepartmentService departmentService) : BaseController
//{
//	private readonly IDepartmentService _departmentService = departmentService;

//	/// <summary>
//	/// Adds a new department to the system.
//	/// </summary>
//	/// <param name="departmentRequestDto">The data transfer object containing department details for creation.</param>
//	/// <remarks>
//	/// Access is limited to users with the "Admin" role.
//	/// </remarks>
//	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

//	[HttpPost]
//    [Authorize(Roles = "Admin")]
//    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
//	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
//	public async Task<Result> AddDepartment(DepartmentRequestDto departmentRequestDto)
//	{
//		return await _departmentService.AddDepartmentAsync(departmentRequestDto);
//	}


//	/// <summary>
//	/// get all departments in the system.
//	/// </summary>
//	/// <remarks>
//	/// Access is limited to users with the "Admin" role.
//	/// </remarks>
//	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
//	[HttpGet]
//    [Authorize(Roles = "Admin,Manager")]
//    [ProducesResponseType(typeof(Result<List<DepartmentResponseDto>>), StatusCodes.Status200OK)]
//	public async Task<Result<List<DepartmentResponseDto>>> GetAllDepartments()
//	{
//		return await _departmentService.GetAllDepartmentsAsync();
//	}
//	/// <summary>
//	/// get all departments in the system.
//	/// </summary>
//	///<param name="id">id of department.</param>
//	/// <remarks>
//	/// Access is limited to users with the "Admin" role.
//	/// </remarks>
//	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
//	[HttpGet("{id}")]
//    [Authorize(Roles = "Admin,Manager")]
//    [ProducesResponseType(typeof(Result<DepartmentResponseDto>), StatusCodes.Status200OK)]
//	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
//	public async Task<Result<DepartmentResponseDto>> GetDepartmentById(int id)
//	{
//		return await _departmentService.GetDepartmentByIdAsync(id);
//	}

//	/// <summary>
//	/// get  department by id in the system.
//	/// </summary>
//	///<param name="id">id of department.</param>
//	///<param name="departmentRequestDto">department dto.</param>
//	/// <remarks>
//	/// Access is limited to users with the "Admin" role.
//	/// </remarks>
//	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

//	[HttpPut("{id}")]
//    [Authorize(Roles = "Admin")]
//    [ProducesResponseType(typeof(Result<DepartmentResponseDto>), StatusCodes.Status200OK)]
//	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
//	public async Task<Result<DepartmentResponseDto>> UpdateDepartment(int id, DepartmentRequestDto departmentRequestDto)
//	{
//		return await _departmentService.UpdateDepartmentAsync(id, departmentRequestDto);
//	}
//	/// <summary>
//	/// search  department by text in the system.
//	/// </summary>
//	///<param name="text">id</param>
//	/// <remarks>
//	/// Access is limited to users with the "Admin" role.
//	/// </remarks>
//	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

//	[HttpGet("search/{text}")]
//    [Authorize(Roles = "Admin,Manager")]
//    [ProducesResponseType(typeof(Result<DepartmentResponseDto>), StatusCodes.Status200OK)]
//	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
//	public async Task<Result<List<DepartmentResponseDto>>> SerachDepartmentByText(string text)
//	{
//		return await _departmentService.SearchDepartmentByTextAsync(text);
//	}

//	/// <summary>
//	/// get all   department by text in the system.
//	/// </summary>
//	///<param name="text">id</param>
//	/// <remarks>
//	/// Access is limited to users with the "Admin" role.
//	/// </remarks>
//	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

//	[HttpGet("searchByRelation/{id}")]
//    [Authorize(Roles = "Admin,Manager")]
//    [ProducesResponseType(typeof(Result<DepartmentResponseDto>), StatusCodes.Status200OK)]
//	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
//	public async Task<Result<List<DepartmentResponseDto>>> SearchDepartmentByRelation(int id)
//	{
//		return await _departmentService.GetAllEmployeesForSpecificDepartmentAsync(id);
//	}

//    /// <summary>
//    /// delete  department by id from the system.
//    /// </summary>
//    ///<param name="id">id</param>
//    /// <remarks>
//    /// Access is limited to users with the "Admin" role.
//    /// </remarks>
//    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

//    [HttpDelete("{id}")]
//    [Authorize(Roles = "Admin")]
//    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
//	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
//	public async Task<Result> DeleteDepartment(int id)
//	{
//		return await _departmentService.DeleteDepartmentAsync(id);
//	}
//}