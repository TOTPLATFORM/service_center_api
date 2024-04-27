using ServiceCenter.Core.CustomExceptions;
using ServiceCenter.Core.Result;
using Microsoft.AspNetCore.Diagnostics;

namespace ServiceCenter.API.ExceptionHandlers;

public class AuthorizationExceptionHandler : IExceptionHandler
{
    private readonly ILogger<AuthorizationExceptionHandler> _logger;

    public AuthorizationExceptionHandler(ILogger<AuthorizationExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is AuthorizationException)
        {
            _logger.LogError(
            exception, "Exception occurred: Unauthorized");

            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

            await httpContext.Response
                .WriteAsJsonAsync(Result.Unauthorized());

            return true;
        }
        return false;
    }
}
