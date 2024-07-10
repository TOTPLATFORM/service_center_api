using ServiceCenter.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;
using ServiceCenter.Core.Entities;

namespace ServiceCenter.API.Controllers;

public class ContactController(IContactService contactService) : BaseController
{
	private readonly IContactService _contactService = contactService;

	/// <summary>
	/// Adds a new contact to the system.
	/// </summary>
	/// <param name="contactRequestDto">The data transfer object containing contact details for creation.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin,Manager" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> AddContact(ContactRequestDto contactRequestDto)
	{
		return await _contactService.AddContactAsync(contactRequestDto);
	}	
	/// <summary>
	/// get all contacts in the system.
	/// </summary>
	/// <remarks>
	/// Access is limited to users with the "Admin,Manager" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<ContactResponseDto>>), StatusCodes.Status200OK)]
	public async Task<Result<PaginationResult<ContactResponseDto>>> GetAllContacts(int itemCount , int index)
	{
		return await _contactService.GetAllContactsAsync(itemCount,index);
	}

    /// <summary>
    /// action for update an contact status that take contact status and conatct id.
    /// </summary>
    /// <param name="id">contact id.</param>
    /// <param name="status">contact status</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Sales,Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>

    [HttpPut("contactId/{id}/status/{status}")]
    [Authorize(Roles = "Admin,Sales,Manager")]
	[ProducesResponseType(typeof(Result<ContactResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<ContactResponseDto>> UpdateContactStatus(ContactStatus status, Guid id)
	{
		return await _contactService.UpdateContactStatusAsync(id, status);
	}
    /// <summary>
    /// get contact in the system.
    /// </summary>
    ///<param name="id">id of contact.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,MAnager" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<ContactResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<ContactResponseDto>> GetContactById(Guid id)
    {
        return await _contactService.GetContacttByIdAsync(id);
    }
    /// <summary>
    /// update  contact in the system.
    /// </summary>
    ///<param name="id">id of contact.</param>
    ///<param name="contactRequestDto">contact dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<ContactResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ContactResponseDto>> UpdateContact(Guid id, ContactRequestDto contactRequestDto)
    {
        return await _contactService.UpdateContactAsync(id, contactRequestDto);
    }
    /// <summary>
    /// search  contact by text in the system.
    /// </summary>
    ///<param name="text">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Manager" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpGet("search/{text}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<ContactResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<ContactResponseDto>>> SerachContactByText(string text, int itemCount, int index)
    {
        return await _contactService.SearchContactByTextAsync(text, itemCount, index);
    }

}
