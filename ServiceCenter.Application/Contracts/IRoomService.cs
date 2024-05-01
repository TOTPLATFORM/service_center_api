﻿using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IRoomService : IApplicationService, IScopedService
{
    /// <summary>
    /// function to add  Room  that take  RoomDto   
    /// </summary>
    /// <param name="RoomRequestDto">Room  request dto</param>
    /// <returns> Room  added successfully </returns>
    public Task<Result> AddRoomAsync(RoomRequestDto RoomRequestDto);
    /// <summary>
    /// function to get all Room  
    /// </summary>
    /// <returns>list all Room  response dto </returns>
    public Task<Result<List<RoomResponseDto>>> GetAllRoomAsync();
    /// <summary>
    /// function to get  Room  by id that take   Room id
    /// </summary>
    /// <param name="id"> Room  id</param>
    /// <returns> Room  response dto</returns>
    public Task<Result<RoomResponseDto>> GetRoomByIdAsync(int id);
}
