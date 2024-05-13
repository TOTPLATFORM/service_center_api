using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;

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
	///// <summary>
	///// get all contacts in the system.
	///// </summary>
	/////<param name="id">id of contact.</param>
	///// <remarks>
	///// Access is limited to users with the "Admin" role.
	///// </remarks>
	///// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	//[HttpGet("{id}")]
	////[Authorize(Roles = "Admin")]
	//[ProducesResponseType(typeof(Result<ContactResponseDto>), StatusCodes.Status200OK)]
	//[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	//public async Task<Result<ContactResponseDto>> GetContactById(int id)
	//{
	//	return await _contactService.GetContactByIdAsync(id);
	//}

	///// <summary>
	///// get  contact by id in the system.
	///// </summary>
	/////<param name="id">id of contact.</param>
	/////<param name="contactRequestDto">contact dto.</param>
	///// <remarks>
	///// Access is limited to users with the "Admin" role.
	///// </remarks>
	///// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	//[HttpPut("{id}")]
	////[Authorize(Roles = "Admin")]
	//[ProducesResponseType(typeof(Result<ContactResponseDto>), StatusCodes.Status200OK)]
	//[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	//public async Task<Result<ContactResponseDto>> UpdateInventory(int id, ContactRequestDto contactRequestDto)
	//{
	//	return await _contactService.UpdateContactAsync(id, contactRequestDto);
	//}
	///// <summary>
	///// search  contact by text in the system.
	///// </summary>
	/////<param name="text">id</param>
	///// <remarks>
	///// Access is limited to users with the "Admin" role.
	///// </remarks>
	///// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	//[HttpGet("search/{text}")]
	////[Authorize(Roles = "Admin")]
	//[ProducesResponseType(typeof(Result<ContactResponseDto>), StatusCodes.Status200OK)]
	//[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	//public async Task<Result<List<ContactResponseDto>>> SerachInventoryByText(string text)
	//{
	//	return await _contactService.SearchContactByTextAsync(text);
	//}
	///// <summary>
	///// delete  contact by id from the system.
	///// </summary>
	/////<param name="id">id</param>
	///// <remarks>
	///// Access is limited to users with the "Admin" role.
	///// </remarks>
	///// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	//[HttpDelete]
	////[Authorize(Roles = "Admin")]
	//[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	//[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	//public async Task<Result> DeleteInventoryAsycn(int id)
	//{
	//	return await _contactService.DeleteContactAsync(id);
	//}
}
