﻿using ServiceCenter.Core.CustomExceptions;
using ServiceCenter.Core.Result;
using Microsoft.AspNetCore.Diagnostics;

namespace ServiceCenter.API.ExceptionHandlers;

public class ResourceNotFoundExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ResourceNotFoundExceptionHandler> _logger;

    public ResourceNotFoundExceptionHandler(ILogger<ResourceNotFoundExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is ResourceNotFoundException notFoundException)
        {
            _logger.LogError(
            exception, $"{notFoundException.ResourceName} Id not found,Id: {notFoundException.Id}");

            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

            await httpContext.Response
                .WriteAsJsonAsync(Result.NotFound($"No {notFoundException.ResourceName} Found with Id: {notFoundException.Id}"));

            return true;
        }
        return false;
    }
}
