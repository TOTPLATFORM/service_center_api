using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface ICampaginService : IApplicationService, IScopedService
{
	/// <summary>
	/// function to add campagin that take campaginDto   
	/// </summary>
	/// <param name="campaginRequestDto">time slot request dto</param>
	/// <returns>Campagin added successfully </returns>
	public Task<Result> AddCampaginAsync(CampaginRequestDto campaginRequestDto);

}
