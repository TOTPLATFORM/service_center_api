using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface ITransactionService : IApplicationService, IScopedService
{
    /// <summary>
    /// function to add transaction that take transactionDto   
    /// </summary>
    /// <param name="TransactionRequestDto">transaction request dto</param>
    /// <returns>Transaction added successfully </returns>
    public Task<Result> AddTransactionAsync(TransactionRequestDto transactionRequestDto);
}
