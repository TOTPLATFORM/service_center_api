using ServiceCenter.Application.DTOS;
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
    public Task<Result<List<ComplaintResponseDto>>> GetAllComplaintsAsync();

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
    public Task<Result<ComplaintResponseDto>> UpdateComplaintAsync(int id, ComplaintRequestDto ComplaintRequestDto);

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
    public Task<Result<List<ComplaintResponseDto>>> SearchComplaintByTextAsync(Status text);

    /// <summary>
    /// asynchronously retrieves complaints by customer unique identifier.
    /// </summary>
    /// <param name="customerId">the unique identifier of the customer to retrieve its complaints.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the get all complaints by customer id operation.</returns>
	public Task<Result<List<ComplaintResponseDto>>> GetComplaintsByCustomerAsync(string customerId);

}
