using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IContactService : IApplicationService, IScopedService
{
	/// <summary>
	/// function to add contact that take contactDto   
	/// </summary>
	/// <param name="contactRequestDto">contact request dto</param>
	/// <returns>Contact added successfully </returns>
	public Task<Result> AddContactAsync(ContactRequestDto contactRequestDto);

	/// <summary>
	/// function to get all contacts 
	/// </summary>
	/// <returns>list all contactResponseDto </returns>
	public Task<Result<List<ContactResponseDto>>> GetAllContactsAsync();

	/// <summary>
	/// function to update contact that take contactRequestDto   
	/// </summary>
	/// <param name="id">contact id</param>
	///   <param name="status">contact status</param>
	/// <returns>Updated contact </returns>
	public Task<Result<ContactResponseDto>> UpdateContactStatusAsync(int id, ContactStatus status);

}
