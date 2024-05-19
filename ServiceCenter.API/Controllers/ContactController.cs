using HMSWithLayers.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;

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
	//[Authorize(Roles = "Admin")]
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
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpGet]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<List<ContactResponseDto>>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<List<ContactResponseDto>>> GetAllContacts()
	{
		return await _contactService.GetAllContactsAsync();
	}

	/// <summary>
	/// action for update an contact status that take contact status and conatct id.
	/// </summary>
	/// <param name="id">contact id.</param>
	/// <param name="status">contact status</param>
	/// <returns>result of the contact response dto after updated successfully</returns>

	[HttpPut("contactId/{id}/status/{status}")]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<ContactResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<ContactResponseDto>> UpdateContactStatus(ContactStatus status, int id)
	{
		return await _contactService.UpdateContactStatusAsync(id, status);
	}
}
