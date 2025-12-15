using Core.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Core.Extensions;

public static class TrafficAuditMiddlewareExtension
{
    public static IApplicationBuilder UseTrafficAuditMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TrafficAuditMiddleware>();
    }
}