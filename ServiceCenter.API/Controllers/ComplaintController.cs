using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;
public class ComplaintController(IComplaintService ComplaintService) : BaseController
{
    private readonly IComplaintService _ComplaintService = ComplaintService;

    /// <summary>
    /// action for add Complaint  action that take  Complaint dto   
    /// </summary>
    /// <param name="ComplaintDto">Complaint  dto</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>result for Complaint  added successfully.</returns>
    [HttpPost]
    [Authorize(Roles = "Customer")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddComplaint(ComplaintRequestDto ComplaintDto)
    {
        return await _ComplaintService.AddComplaintAsync(ComplaintDto);
    }

    /// <summary>
    /// get all Complaint categories in the system.
    /// </summary>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result<List<ComplaintResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<List<ComplaintResponseDto>>> GetAllComplaints()
    {
        return await _ComplaintService.GetAllComplaintsAsync();
    }

    /// <summary>
    /// get Complaint by id in the system.
    /// </summary>
    ///<param name="id">id of Complaint.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<ComplaintResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ComplaintResponseDto>> GetComplaintById(int id)
    {
        return await _ComplaintService.GetComplaintByIdAsync(id);
    }

    /// </summary>
    ///<param name="id">id of Complaint.</param>
    ///<param name="ComplaintRequestDto">Complaint dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Customer,Manager")]
    [ProducesResponseType(typeof(Result<ComplaintResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ComplaintResponseDto>> UpdateComplaint(int id, ComplaintRequestDto ComplaintRequestDto)
    {
        return await _ComplaintService.UpdateComplaintAsync(id, ComplaintRequestDto);
    }
    /// <summary>
    /// delete  Complaint  by id from the system.
    /// </summary>
    ///<param name="id">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Customer")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteComplaintAsycn(int id)
    {
        return await _ComplaintService.DeleteComplaintAsync(id);
    }

	[HttpGet("searchByComplaints/{customerId}")]
    [Authorize(Roles = "Customer,Admin,Manager")]
    [ProducesResponseType(typeof(Result<ComplaintResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<List<ComplaintResponseDto>>> GetComplaintsByCustomer(string customerId)
	{
		return await _ComplaintService.GetComplaintsByCustomerAsync(customerId);
	}
}
