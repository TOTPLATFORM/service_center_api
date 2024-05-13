using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
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
	/// <returns>Branch added successfully </returns>
	public Task<Result> AddContactAsync(ContactRequestDto contactRequestDto);
}
