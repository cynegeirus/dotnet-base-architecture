using Core.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Core.Extensions;

public static class TransactionMiddlewareExtension
{
    public static IApplicationBuilder UseTransactionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TransactionMiddleware>();
    }
}