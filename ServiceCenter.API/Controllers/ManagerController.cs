﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class ManagerController(IManagerService managerService) : BaseController
{
	private readonly IManagerService _managerService = managerService;

	/// <summary>
	/// Adds a new manager to the system.
	/// </summary>
	/// <param name="managerRequestDto">The data transfer object containing manager details for creation.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpPost]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> AddManager(ManagerRequestDto managerRequestDto)
	{
		return await _managerService.AddManagerAsync(managerRequestDto);
	}

	/// <summary>
	/// get all Managers in the system.
	/// </summary>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpGet]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<PaginationResult<ManagerResponseDto>>), StatusCodes.Status200OK)]
	public async Task<Result<PaginationResult<ManagerResponseDto>>> GetAllManagers(int itemCount, int index)
	{
		return await _managerService.GetAllManagersAsync(itemCount, index);
	}

	/// <summary>
	/// get manager in the system.
	/// </summary>
	///<param name="id">id of manager.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpGet("{id}")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<ManagerGetByIdResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
	public async Task<Result<ManagerGetByIdResponseDto>> GetManagerById(string id)
	{
		return await _managerService.GetMangertByIdAsync(id);
	}

	/// <summary>
	/// updates an existing manager's information.
	/// </summary>
	/// <param name="id">the unique identifier of the manager  to update.</param>
	/// <param name="managerRequestDto">the data transfer object containing updated details for the manager.</param>
	/// <remarks>
	/// access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
	[HttpPut("{id}")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<ManagerGetByIdResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
	public async Task<Result<ManagerGetByIdResponseDto>> UpdateManager(string id, ManagerRequestDto managerRequestDto)
	{
		return await _managerService.UpdateManagerAsync(id, managerRequestDto);
	}

	/// <summary>
	/// search  manager by text in the system.
	/// </summary>
	///<param name="text">id</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpGet("search")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<ManagerResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<PaginationResult<ManagerResponseDto>>> SerachManagerByText(string text, int itemCount, int index)
	{
		return await _managerService.SearchManagerByTextAsync(text, itemCount, index);
	}
}
