using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;


public class ContractController(IContractService ContractService) : BaseController
{
    private readonly IContractService _ContractService = ContractService;

    /// <summary>
    /// action for add Contract action that take Contract dto   
    /// </summary>
    /// <param name="ContractDto">Contract dto</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>result for Contract  added successfully.</returns>
    [HttpPost]
    [Authorize(Roles = "Customer")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddContract(ContractRequestDto ContractDto)
    {
        return await _ContractService.AddContractAsync(ContractDto);
    }


    /// <summary>
    /// get all Contract in the system.
    /// </summary>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet]
    [Authorize(Roles = "Employee,Manager,Customer")]
    [ProducesResponseType(typeof(Result<List<ContractResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<List<ContractResponseDto>>> GetAllContract()
    {
        return await _ContractService.GetAllContractAsync();
    }
    /// <summary>
    /// get Contract by id in the system.
    /// </summary>
    ///<param name="id">id of Contract.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Employee,Manager,Customer")]
    [ProducesResponseType(typeof(Result<ContractResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<ContractResponseDto>> GetContractById(int id)
    {
        return await _ContractService.GetContractByIdAsync(id);
    }
    /// </summary>
    ///<param name="id">id of Contract.</param>
    ///<param name="ContractRequestDto">Contract dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Employee,Manager")]
    [ProducesResponseType(typeof(Result<ContractResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ContractResponseDto>> UpdateContract(int id, ContractRequestDto ContractRequestDto)
    {
        return await _ContractService.UpdateContractAsync(id, ContractRequestDto);
    }
    /// <summary>
    /// delete  Contract  by id from the system.
    /// </summary>
    ///<param name="id">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteContractAsycn(int id)
    {
        return await _ContractService.DeleteContractAsync(id);
    }

}
