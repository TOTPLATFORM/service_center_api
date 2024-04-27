using ServiceCenter.API.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using ServiceCenter.Core.Result;
using ServiceCenter.Application.ExtensionMethods;

namespace ServiceCenter.API.ExtensionMethods;


public static class CenterServices
{
    public static IServiceCollection AddCenterServices(this IServiceCollection services, IConfiguration configurations)
    {
        services.AddAutoMapper(typeof(MappingProfiles));

        services.AddApplicationService();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = (apiActionContext) =>
            {
                Result validationError = new Result();
                var errors = apiActionContext.ModelState
                .Where(P => P.Value.Errors.Count > 0)
                .SelectMany(P => P.Value.Errors)
                .Select(E => E.ErrorMessage);
                foreach (var error in errors)
                {
                    validationError = Result.Invalid(new List<ValidationError>
                    {
                        new ValidationError
                        {
                            ErrorMessage = error
                        }
                    });
                }

                return new BadRequestObjectResult(validationError);
            };
        });
        //services.AddHangfire(configuration => configuration
        //    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        //    .UseSimpleAssemblyNameTypeSerializer()
        //    .UseRecommendedSerializerSettings()
        //    .UseSqlServerStorage(configurations.GetConnectionString("HangfireConnection")));
        //services.AddHangfireServer();

        return services;
    }
}
