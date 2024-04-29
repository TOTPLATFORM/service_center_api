using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts
{
    public interface IProductBrandService : IApplicationService, IScopedService
    {
        /// <summary>
        /// function to add  product brand that take  productBrandDto   
        /// </summary>
        /// <param name=" productBrandRequestDto">product brand request dto</param>
        /// <returns> product brand added successfully </returns>
        public Task<Result> AddProductBrandAsync( ProductBrandRequestDto  productBrandRequestDto);

    }
}
