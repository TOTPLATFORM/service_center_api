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
    //[Authorize(Roles = "Admin")]
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
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<ContractResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<ContractResponseDto>>> GetAllContract()
    {
        return await _ContractService.GetAllContractAsync();
    }

}
