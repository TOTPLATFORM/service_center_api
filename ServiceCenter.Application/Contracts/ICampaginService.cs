using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface ICampaginService : IApplicationService, IScopedService
{
    /// <summary>
    /// asynchronously adds a new campaign to the database.
    /// </summary>
    /// <param name="campaginRequestDto">the campaign data transfer object containing the details necessary for creation.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the campaign addition.</returns>
	public Task<Result> AddCampaginAsync(CampaginRequestDto campaginRequestDto);

    /// <summary>
    /// asynchronously retrieves all campaigns in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of campaign response DTOs.</returns>
	public Task<Result<List<CampaginResponseDto>>> GetAllCampaginsAsync();

    /// <summary>
    /// asynchronously retrieves a campaign by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the campaign to retrieve.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the campaign response DTO.</returns>
	public Task<Result<CampaginGetByIdResposeDto>> GetCampaginByIdAsync(int id);

    /// <summary>
    /// asynchronously updates the data of an existing campaign.
    /// </summary>
    /// <param name="id">the unique identifier of the campaign to update.</param>
    /// <param name="campaginRequestDto">the campaign data transfer object containing the updated details.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<CampaginGetByIdResposeDto>> UpdateCampaginAsync(int id, CampaginRequestDto campaginRequestDto);

    /// <summary>
    /// asynchronously searches for campaigns based on the provided text.
    /// </summary>
    /// <param name="text">the text to search within campaign data.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of campaign response DTOs that match the search criteria.</returns>
	public Task<Result<List<CampaginResponseDto>>> SearchCampaginByTextAsync(string text);

    /// <summary>
    /// asynchronously updates the status of an existing campaign.
    /// </summary>
    /// <param name="id">the unique identifier of the campaign to update its status.</param>
    /// <param name="status">The updated status</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
    public Task<Result<CampaginGetByIdResposeDto>> UpdateCampaginStatusAsync(int id, CampaginStatus status);

    /// <summary>
    /// asynchronously deletes a campaign from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the campaign to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
	public Task<Result> DeleteCampaginAsync(int id);
}
