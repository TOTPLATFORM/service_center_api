using Serilog;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace ServiceCenter.API.Middleware;

public class AuditLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuditLogMiddleware> _logger;
    private const string ControllerKey = "controller";

    public AuditLogMiddleware(RequestDelegate next, ILogger<AuditLogMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {

        var requestBody = await GetRequestBody(context.Request);
        var originalBodyStream = context.Response.Body;

        using (var responseBodyStream = new MemoryStream())
        {

            context.Response.Body = responseBodyStream;

            await _next(context);

            // Get controller name if available
            context.Request.RouteValues.TryGetValue(ControllerKey, out var controllerValue);
            var controllerName = (string)(controllerValue ?? string.Empty);

            // Log request details
            _logger.LogInformation($"Request:- Host:{context.Request.Host},Path:{context.Request.Path},\n Controller Name:{controllerName},\n Client IP: {context.Connection.RemoteIpAddress} at {DateTimeOffset.UtcNow}");

            if (context.Request.QueryString.HasValue)
            {
                _logger.LogInformation($"Query Parameters: {context.Request.QueryString}");
            }

            // Log request body if available
            if (!string.IsNullOrEmpty(requestBody))
                _logger.LogInformation($"Request Body: {requestBody}");

            // Log response details
            responseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
            _logger.LogInformation($"Response: {context.Response.StatusCode} at: {DateTimeOffset.UtcNow}");
            _logger.LogInformation($"Response Body: {responseBody}");

            // Reset the response body to the original stream
            responseBodyStream.Seek(0, SeekOrigin.Begin);
            await responseBodyStream.CopyToAsync(originalBodyStream);
        }
    }

    private async Task<string> GetRequestBody(HttpRequest request)
    {
        request.EnableBuffering();

        using (var reader = new StreamReader(request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, bufferSize: 1024, leaveOpen: true))
        {
            var requestBody = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            return requestBody;
        }
    }
}
