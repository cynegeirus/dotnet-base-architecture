using System.Text.Json;
using Core.ContextKeys;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Middlewares;

public class GlobalExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            context.Items[AuditContextKey.AuditTraceException] = ex;
            await WriteProblemDetailsResponse(context, ex);
        }
    }

    private static async Task WriteProblemDetailsResponse(HttpContext context, Exception ex)
    {
        ErrorLogger.LogError($"[{nameof(GlobalExceptionMiddleware)}] Unhandled exception", ex);

        var traceId = context.Items.TryGetValue(AuditContextKey.AuditTraceId, out var id) ? id?.ToString() : context.TraceIdentifier;

        var problemDetails = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7807",
            Title = "Internal Server Error",
            Status = StatusCodes.Status500InternalServerError,
            Detail = "An unexpected error occurred while processing your request.",
            Instance = context.Request.Path
        };

        problemDetails.Extensions["traceId"] = traceId;

        context.Response.Clear();
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        }));
    }
}