using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

/// <summary>
/// provides an interface for complaint-related services that manages complaint data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IComplaintService : IApplicationService, IScopedService
{
    /// <summary>
    /// asynchronously adds a new complaint to the database.
    /// </summary>
    /// <param name="complaintRequestDto">the complaint data transfer object containing the details necessary for creation.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the complaint addition.</returns>
    public Task<Result> AddComplaintAsync(ComplaintRequestDto ComplaintRequestDto);

    /// <summary>
    /// asynchronously retrieves all complaints in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of complaint response DTOs.</returns>
    public Task<Result<PaginationResult<ComplaintResponseDto>>> GetAllComplaintsAsync(int itemCount, int index);

    /// <summary>
    /// asynchronously retrieves a complaint by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the complaint to retrieve.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the complaint response DTO.</returns>
    public Task<Result<ComplaintResponseDto>> GetComplaintByIdAsync(int id);

    /// <summary>
    /// asynchronously updates the data of an existing complaint.
    /// </summary>
    /// <param name="id">the unique identifier of the complaint to update.</param>
    /// <param name="complaintRequestDto">the complaint data transfer object containing the updated details.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
    public Task<Result<ComplaintResponseDto>> UpdateComplaintStatusAsync(int id, Status complaintStatus);

    /// <summary>
    /// asynchronously deletes a complaint from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the complaint to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
    public Task<Result> DeleteComplaintAsync(int id);

    /// <summary>
    /// asynchronously searches for complaints based on the provided text.
    /// </summary>
    /// <param name="text">the text to search within complaint data.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of complaint response DTOs that match the search criteria.</returns>
    public Task<Result<PaginationResult<ComplaintResponseDto>>> SearchComplaintByStatusAsync(Status text, int itemCount, int index);

    /// <summary>
    /// asynchronously retrieves complaints by customer unique identifier.
    /// </summary>
    /// <param name="customerId">the unique identifier of the customer to retrieve its complaints.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the get all complaints by customer id operation.</returns>
    public Task<Result<PaginationResult<ComplaintResponseDto>>> GetComplaintsForSpecificCustomerAsync(string customerId, int itemCount, int index);
    /// <summary>
    /// asynchronously retrieves complaints by ServiceProvider unique identifier.
    /// </summary>
    /// <param name="serviceProviderId">the unique identifier of the ServiceProvider  to retrieve its complaints.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the get all complaints by ServiceProvider id operation.</returns>

    public Task<Result<PaginationResult<ComplaintResponseDto>>> GetComplaintsForSpecificServiceProviderAsync(string serviceProviderId, int itemCount, int index);
    /// <summary>
    /// asynchronously retrieves complaints by branch unique identifier.
    /// </summary>
    /// <param name="branchId">the unique identifier of the branch  to retrieve its complaints.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the get all complaints by branch id operation.</returns>

    public Task<Result<PaginationResult<ComplaintResponseDto>>> GetComplaintsForSpecificBranchAsync(int branchId, int itemCount, int index);
}