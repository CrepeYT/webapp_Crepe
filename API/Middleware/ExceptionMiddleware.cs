using System.Net;
using System.Text.Json;
using API.Errors;
namespace API.Middleware;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _RequestDelegate;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _HostEnvironment;
    public ExceptionMiddleware(RequestDelegate RequestDelegate, ILogger<ExceptionMiddleware> logger, IHostEnvironment HostEnvironment)
    {
        _RequestDelegate = RequestDelegate;
        _logger = logger;
        _HostEnvironment = HostEnvironment;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _RequestDelegate(httpContext);
        }
        catch (System.Exception e)
        {
            _logger.LogError(e, e.Message);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = _HostEnvironment.IsDevelopment()
                ? new ApiException(httpContext.Response.StatusCode, e.Message, e.StackTrace?.ToString())
                : new ApiException(httpContext.Response.StatusCode, e.Message, "InternalServerError");
            var opt = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, opt);
            await httpContext.Response.WriteAsync(json);
        }
    }
}