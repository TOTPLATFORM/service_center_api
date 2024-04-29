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

		/// <summary>
		/// function to get all timeslot 
		/// </summary>
		/// <returns>list all time slot response dto </returns>
		public Task<Result<List<TimeSlotResponseDto>>> GetAllTimeSlotAsync();

		/// <summary>
		/// function to get time slot by id that take  time slote id
		/// </summary>
		/// <param name="id">time slot id</param>
		/// <returns>time slot response dto</returns>
		public Task<Result<TimeSlotResponseDto>> GetTimeSlotByIdAsync(int id);

		/// <summary>
		/// function to update TimeSlot that take timeSlotDto   
		/// </summary>
		/// <param name="id">time slot id</param>
		/// <param name="timeSlotRequestDto">timeSlot dto</param>
		/// <returns>Updated TimeSlot </returns>
		public Task<Result<TimeSlotResponseDto>> UpdateTimeSlotAsync(int id, TimeSlotRequestDto timeSlotRequestDto);

		/// <summary>
		/// function to delete TimeSlot that take timeSlotDto   
		/// </summary>
		/// <param name="id">time slot id</param>
		/// <returns>TimeSlot removed successfully </returns>
		public Task<Result> DeleteTimeSlotAsync(int id);
	}
}
