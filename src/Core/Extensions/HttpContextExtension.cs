using System.Net;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions;

public static class HttpContextExtension
{
    public static IPAddress GetRemoteIPAddress(this HttpContext context, bool allowForwarded = true)
    {
        if (allowForwarded)
        {
            var header = context.Request.Headers["CF-Connecting-IP"].FirstOrDefault() ?? context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (IPAddress.TryParse(header, out var ip))
                return ip;
        }

        return context.Connection.RemoteIpAddress;
    }
}