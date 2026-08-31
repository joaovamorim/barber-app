using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Barber.App.Api.Middlewares;
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger = Log.ForContext<GlobalExceptionMiddleware>();

    public GlobalExceptionMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Unhandled exception");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var result = JsonSerializer.Serialize(new
            {
                error = "Ocorreu um erro interno."
            });

            await context.Response.WriteAsync(result);
        }
    }
}
