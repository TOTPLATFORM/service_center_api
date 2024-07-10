using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;

namespace ServiceCenter.API.Controllers;
public class ComplaintController(IComplaintService ComplaintService) : BaseController
{
    private readonly IComplaintService _ComplaintService = ComplaintService;

    /// <summary>
    /// action for add Complaint  action that take  Complaint dto   
    /// </summary>
    /// <param name="ComplaintDto">Complaint  dto</param>
    /// <remarks>
    /// Access is limited to users with the "Customer" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    [Authorize(Roles = "Customer")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddComplaint(ComplaintRequestDto ComplaintDto)
    {
        return await _ComplaintService.AddComplaintAsync(ComplaintDto);
    }

    /// <summary>
    /// retrieves all complaint in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of complaint to retrieve</param>
    ///<param name="index">index of complaint to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin,Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all complaint.</returns> [HttpGet]
    [HttpGet]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result<PaginationResult<ComplaintResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ComplaintResponseDto>>> GetAllComplaints(int itemCount, int index)
    {
        return await _ComplaintService.GetAllComplaintsAsync(itemCount, index);
    }

    /// <summary>
    /// retrieves a complaint  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the complaint .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Manager" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the complaint category details.</returns>[HttpGet("{id}")]
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
    /// Access is limited to users with the "Admin,Manager" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>

    [HttpPut("complaintId/{id}/status/{ComplaintStatus}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<ComplaintResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ComplaintResponseDto>> UpdateComplaint(int id, Status ComplaintStatus)
    {
        return await _ComplaintService.UpdateComplaintStatusAsync(id, ComplaintStatus);
    }


    /// <summary>
    /// deletes a complaint from the system by their unique identifier.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin,Customer" role.
    /// </remarks>
    /// <param name="id">the unique identifier of the complaint to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Customer")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteComplaintAsync(int id)
    {
        return await _ComplaintService.DeleteComplaintAsync(id);
    }
    /// <summary>
    ///  retrieves complaint by their customer unique identifier.
    /// </summary>
    ///<param name="customerId">the unique identifier of the customer</param>
    /// <param name = "itemCount" > item count of complaint to retrieve</param>
    ///<param name="index">index of complaint to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Manager,Admin,Customer" role.
    /// </remarks>
    /// <returns>>a task that represents the asynchronous operation, which encapsulates the result containing the customer's complaint.</returns>
    [HttpGet("searchByCustomer/{customerId}")]
    [Authorize(Roles = "Customer,Admin,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<ComplaintResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<ComplaintResponseDto>>> GetComplaintsByCustomer(string customerId, int itemCount, int index)
    {
        return await _ComplaintService.GetComplaintsForSpecificCustomerAsync(customerId, itemCount, index);
    }
    /// <summary>
    /// retrieves complaint by their branch unique identifier.
    /// </summary>
    ///<param name="branchId">the unique identifier of the branch</param>  
    /// <param name = "itemCount" > item count of complaint to retrieve</param>
    ///<param name="index">index of complaint to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Manager,Admin" role.
    /// </remarks>
    /// <returns>>a task that represents the asynchronous operation, which encapsulates the result containing the branch's complaints.</returns>

    [HttpGet("searchByBranch/{branchId}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<ComplaintResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<ComplaintResponseDto>>> GetComplaintsByBranch(int branchId, int itemCount, int index)
    {
        return await _ComplaintService.GetComplaintsForSpecificBranchAsync(branchId, itemCount, index);
    }
    /// <summary>
    /// retrieves complaint by their service provider unique identifier.
    /// </summary>
    ///<param name="serviceProviderId">the unique identifier of the service provider</param>  
    /// <param name = "itemCount" > item count of complaint to retrieve</param>
    ///<param name="index">index of complaint to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Manager,Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the service provider's complaint.</returns>

    [HttpGet("searchByServiceProvider/{serviceProviderId}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<ComplaintResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<ComplaintResponseDto>>> GetComplaintsByServiceProvider(string serviceProviderId, int itemCount, int index)
    {
        return await _ComplaintService.GetComplaintsForSpecificServiceProviderAsync(serviceProviderId, itemCount, index);
    }
}
