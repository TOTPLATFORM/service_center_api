using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace ServiceCenter.Application.Contracts;

public interface IAuthService: IApplicationService, IScopedService
{
    //Task<Result<LoginResponseDto>> Login(LoginRequestDto LoginRequestDto);
    Task<Result> RegisterCustomerAsync(CustomerRequestDto customerRequestDto);
    //Task<Result> RegisterDoctorAsync(DoctorRequestDto doctorDto);
    //Task<Result> RegisterPharmacistAsync(PharmacistRequestDto pharmacistDto);
    //Task<Result> RegisterLaboratoristAsync(LaboratoriestRequestDto laboratoristDto);
    //Task<Result> RegisterNurseAsync(NurseRequestDto nurseDto);
    Task<Result> RegisterEmployeeAsync(EmployeeRequestDto employeeRequestDto);
    Task<Result> AddUserToRoleAsync(string userId, string roleName);
    Task InitializeRoles();
    Task CreateManagerAccount();
}
