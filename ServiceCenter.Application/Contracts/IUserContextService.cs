using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IUserContextService:IApplicationService, IScopedService
{
    public string UserId { get; set; }
    public string Email { get; set; }
}
