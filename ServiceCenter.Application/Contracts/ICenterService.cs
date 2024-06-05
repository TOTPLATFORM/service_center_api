﻿using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface ICenterService : IApplicationService, IScopedService
{
    /// <summary>
    /// asynchronously adds a new center to the database.
    /// </summary>
    /// <param name="centerRequestDto">the center data transfer object containing the details necessary for creation.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the center addition.</returns>
	public Task<Result> AddCenterAsync(CenterRequestDto centerRequestDto);

    /// <summary>
    /// asynchronously retrieves a center by .
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the center response DTO.</returns>
	public Task<Result<CenterResponseDto>> GetCenterAsync();

    /// <summary>
    /// asynchronously updates the data of an existing center.
    /// </summary>
    /// <param name="id">the unique identifier of the center to update.</param>
    /// <param name="centerRequestDto">the center data transfer object containing the updated details.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<CenterResponseDto>> UpdateCenterAsync(int id, CenterRequestDto centerRequestDto);

    /// <summary>
    /// asynchronously deletes a center from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the center to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
	public Task<Result> DeleteCenterAsync(int id);
}
