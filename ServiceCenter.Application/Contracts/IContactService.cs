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
/// provides an interface for contact-related services that manages contact data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IContactService : IApplicationService, IScopedService
{
    /// <summary>
    /// asynchronously adds a new contact to the database.
    /// </summary>
    /// <param name="contactRequestDto">the contact data transfer object containing the details necessary for creation.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the contact addition.</returns>
	public Task<Result> AddContactAsync(ContactRequestDto contactRequestDto);

	/// <summary>
	/// asynchronously retrieves all contacts in the system.
	/// </summary>
	/// <param name = "itemCount" > item count of contacts to retrieve</param>
	///<param name="index">index of contacts to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of contact response DTOs.</returns>
	public Task<Result<PaginationResult<ContactResponseDto>>> GetAllContactsAsync(int itemCount,int index);

    /// <summary>
    /// asynchronously updates the status of an existing contact.
    /// </summary>
    /// <param name="id">the unique identifier of the contact to update its status.</param>
    /// <param name="status">The updated status</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<ContactResponseDto>> UpdateContactStatusAsync(Guid id, ContactStatus status);

    /// <summary>
    /// asynchronously updates the data of an existing contact.
    /// </summary>
    /// <param name="id">the unique identifier of the contact to update.</param>
    /// <param name="contactRequestDto">the contact data transfer object containing the updated details.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
    public Task<Result<ContactResponseDto>> UpdateContactAsync(Guid id, ContactRequestDto contactRequestDto);
    /// <summary>
    /// asynchronously retrieves a contact by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the contact to retrieve.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the contact response DTO.</returns>
    public Task<Result<ContactResponseDto>> GetContacttByIdAsync(Guid id);
	/// <summary>
	/// asynchronously searches for contacts based on the provided text.
	/// </summary>
	/// <param name="text">the text to search within contact data.</param>
	/// <param name = "itemCount" > item count of contacts to retrieve</param>
	///<param name="index">index of contacts to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of contact response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<ContactResponseDto>>> SearchContactByTextAsync(string text, int itemCount, int index);
}
