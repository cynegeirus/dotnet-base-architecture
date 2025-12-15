using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions;

public static class HttpContextExtension
{
    extension(HttpContext context)
    {
        public Guid? GetLoggedUserId()
        {
            if (context?.User?.Identity?.IsAuthenticated != true)
                return null;

            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            return Guid.TryParse(userIdClaim?.Value, out var userId)
                ? userId
                : null;
        }

        public IPAddress? GetRemoteIpAddress(bool allowForwarded = true)
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
}