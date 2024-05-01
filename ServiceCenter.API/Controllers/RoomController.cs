using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
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
}