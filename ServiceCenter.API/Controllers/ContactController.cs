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
	/// Access is limited to users with the "Admin" role.
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
	/// Register a new customer  to the system.
	/// </summary>
	/// <param name="customerRequestDto">The data transfer object containing contact details for creation.</param>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpPost("registerCustomer")]
	[ProducesResponseType(typeof(Result<ContactResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<ContactResponseDto>> RegisterCustomer(CustomerRequestDto customerRequestDto)
	{
		return await _contactService.RegisterCustomerAsync(customerRequestDto);
	}

	/// <summary>
	/// get all contacts in the system.
	/// </summary>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpGet]
    [Authorize(Roles = "Admin,Manager,Sales")]
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
	/// <returns>result of the contact response dto after updated successfully</returns>

	[HttpPut("contactId/{id}/status/{status}")]
    [Authorize(Roles = "Admin,Sales")]
	[ProducesResponseType(typeof(Result<ContactResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<ContactResponseDto>> UpdateContactStatus(ContactStatus status, string id)
	{
		return await _contactService.UpdateContactStatusAsync(id, status);
	}
}
