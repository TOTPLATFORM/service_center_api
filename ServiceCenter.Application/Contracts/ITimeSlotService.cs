using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts
{
	public interface ITimeSlotService  : IApplicationService , IScopedService
	{
		/// <summary>
		/// function to add TimeSlot that take timeSlotDto   
		/// </summary>
		/// <param name="timeSlotRequestDto">time slot request dto</param>
		/// <returns>TimeSlot added successfully </returns>
		public Task<Result> AddTimeSlotAsync(TimeSlotRequestDto timeSlotRequestDto);
		
	}
}
