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
    /// function to add Contract that take ContractDto   
    /// </summary>
    /// <param name="ContractRequestDto">Contract request dto</param>
    /// <returns> Contract  added successfully </returns>
    public Task<Result> AddContractAsync(ContractRequestDto ContractRequestDto);

    /// <summary>
    /// function to get all Contract  
    /// </summary>
    /// <returns>list all Contract  response dto </returns>
    public Task<Result<List<ContractResponseDto>>> GetAllContractAsync();
    /// <summary>
    /// function to get  Contract  by id that take   Contract id
    /// </summary>
    /// <param name="id"> Contract  id</param>
    /// <returns> Contract  response dto</returns>
    public Task<Result<ContractResponseDto>> GetContractByIdAsync(int id);

    /// <summary>
    /// function to update Contract  that take ContractRequestDto   
    /// </summary>
    /// <param name="id">Contract id</param>
    /// <param name="ContractRequestDto">Contract dto</param>
    /// <returns>Updated Contract </returns>
    public Task<Result<ContractResponseDto>> UpdateContractAsync(int id, ContractRequestDto ContractRequestDto);
    /// <summary>
    /// function to delete Contract  that take Contract  id   
    /// </summary>
    /// <param name="id">Contract  id</param>
    /// <returns>Contract  removed successfully </returns>
    public Task<Result> DeleteContractAsync(int id);
}
