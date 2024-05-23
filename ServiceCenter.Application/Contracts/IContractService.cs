using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IContractService : IApplicationService, IScopedService
{
    /// <summary>
    /// asynchronously adds a new contract to the database.
    /// </summary>
    /// <param name="contractRequestDto">the contract data transfer object containing the details necessary for creation.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the contract addition.</returns>
    public Task<Result> AddContractAsync(ContractRequestDto contractRequestDto);

    /// <summary>
    /// asynchronously retrieves all contracts in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of contract response DTOs.</returns>
    public Task<Result<List<ContractResponseDto>>> GetAllContractAsync();
    /// <summary>
    /// asynchronously retrieves a contract by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the contract to retrieve.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the contract response DTO.</returns>
    public Task<Result<ContractResponseDto>> GetContractByIdAsync(int id);

    /// <summary>
    /// asynchronously updates the data of an existing contract.
    /// </summary>
    /// <param name="id">the unique identifier of the contract to update.</param>
    /// <param name="contractRequestDto">the contract data transfer object containing the updated details.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
    public Task<Result<ContractResponseDto>> UpdateContractAsync(int id, ContractRequestDto contractRequestDto);
    /// <summary>
    /// asynchronously deletes a contract from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the contract to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
    public Task<Result> DeleteContractAsync(int id);
}
