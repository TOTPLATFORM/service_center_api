using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;
/// <summary>
/// provides an interface for warehouse-related services that manages warehouse data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IWareHousManagerService
{
    /// <summary>
    /// asynchronously adds a new warehouse to the database.
    /// </summary>
    /// <param name="wareHouseManagerRequestDto">the warehouse data transfer object containing the details necessary for creation.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the warehouse addition.</returns>
    public Task<Result> AddWareHouseManagerServiceAsync(WareHouseManagerRequestDto wareHouseManagerRequestDto);
    /// <summary>
    /// asynchronously retrieves all warehouse in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of warehouse response DTOs.</returns>
    public Task<Result<List<WareHouseManagerResponseDto>>> GetAllWareHouseManagerServicesAsync();
    /// <summary>
    /// asynchronously retrieves a warehouse by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the warehouse to retrieve.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the warehouse response DTO.</returns>
    public Task<Result<WareHouseManagerResponseDto>> GetWareHouseManagerServiceByIdAsync(string id);
    /// <summary>
    /// asynchronously updates the data of an existing warehouse.
    /// </summary>
    /// <param name="id">the unique identifier of the warehouse to update.</param>
    /// <param name="ratingRequestDto">the warehouse data transfer object containing the updated details.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
    public Task<Result<WareHouseManagerResponseDto>> UpdateWareHouseManagerServiceAsync(string id, WareHouseManagerRequestDto wareHouseManagerRequestDto);
    /// <summary>
    /// asynchronously deletes a warehouse from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the warehouse to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
    public Task<Result> DeleteWareHouseManagerServiceAsync(string id);

    /// <summary>
    /// asynchronously searches for warehouse based on the provided text.
    /// </summary>
    /// <param name="text">the text to search within warehouse data.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of warehouse response DTOs that match the search criteria.</returns>
    public Task<Result<List<WareHouseManagerResponseDto>>> SearchWareHouseManagerByTextAsync(string text);
}
