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
}
