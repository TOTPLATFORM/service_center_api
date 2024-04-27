using ServiceCenter.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ServiceCenter.Application.ExtensionMethods;

public static class ApplicationService
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        var serviceTypes = Assembly.GetExecutingAssembly().ExportedTypes
                .Where(t => typeof(IApplicationService).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToList();

        
        foreach (var serviceType in serviceTypes)
        {
            var interfaceType = serviceType.GetInterface("I" + serviceType.Name);
            var result = serviceType switch
            {
                _ when typeof(IScopedService).IsAssignableFrom(serviceType) => services.AddScoped(interfaceType, serviceType),
                _ when typeof(ITransientService).IsAssignableFrom(serviceType) => services.AddTransient(interfaceType, serviceType),
                _ when typeof(ISingletonService).IsAssignableFrom(serviceType) => services.AddSingleton(interfaceType, serviceType)
            };
        }
        return services;
    }
}
