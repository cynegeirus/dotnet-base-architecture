using Core.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Core.Extensions;

public static class AuditMiddlewareExtension
{
    public static IApplicationBuilder UseAuditMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuditMiddleware>();
    }
}