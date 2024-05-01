using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class RoomController(IRoomService RoomService) : BaseController
{
    private readonly IRoomService _RoomService = RoomService;

    /// <summary>
    /// action for add Room  action that take  Room dto   
    /// </summary>
    /// <param name="RoomDto">Room  dto</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>result for Room  added successfully.</returns>
    [HttpPost]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddRoom(RoomRequestDto RoomDto)
    {
        return await _RoomService.AddRoomAsync(RoomDto);
    }
    /// <summary>
    /// get all Room  in the system.
    /// </summary>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<RoomResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<RoomResponseDto>>> GetAllRoom()
    {
        return await _RoomService.GetAllRoomAsync();
    }
    /// <summary>
    /// get Room by id in the system.
    /// </summary>
    ///<param name="id">id of Room.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("{id}")]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<RoomResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<RoomResponseDto>> GetRoomById(int id)
    {
        return await _RoomService.GetRoomByIdAsync(id);
    }
    /// </summary>
    ///<param name="id">id of Room.</param>
    ///<param name="RoomRequestDto">Room dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpPut("{id}")]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<RoomResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<RoomResponseDto>> UpdateRoom(int id, RoomRequestDto RoomRequestDto)
    {
        return await _RoomService.UpdateRoomAsync(id, RoomRequestDto);
    }
    /// <summary>
    /// delete  Room  by id from the system.
    /// </summary>
    ///<param name="id">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpDelete]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteRoomAsycn(int id)
    {
        return await _RoomService.DeleteRoomAsync(id);
    }
    /// </summary>
    ///<param name="text">bool </param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpGet("search/{text}")]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<RoomResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<RoomResponseDto>>> SearchRoomByText(bool text)
    {
        return await _RoomService.SearchRoomByAvaliabilityAsync(text);
    }
    [HttpGet("searchByCenter/{text}")]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<RoomResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<RoomResponseDto>>> SearchByCenter(string text)
    {
        return await _RoomService.GetAllRoomsForSpecificCenter(text);
    }
}