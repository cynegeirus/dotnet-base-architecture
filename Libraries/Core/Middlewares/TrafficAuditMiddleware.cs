using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using Core.ContextKeys;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entities.Concrete.Log;
using Core.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.Middlewares;

public sealed class TrafficAuditMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var auditTraceId = Guid.NewGuid();
        context.Items[AuditContextKey.AuditTraceId] = auditTraceId;
        context.Response.Headers["X-Audit-Trace-Id"] = auditTraceId.ToString();

        var stopwatch = Stopwatch.StartNew();
        var originalResponseBody = context.Response.Body;
        await using var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        var log = new TrafficAuditLog
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now,
            IsUpdated = false,
            IsDeleted = false,
            AuditTraceId = auditTraceId,
            AuditLoggedDate = DateTime.Now,
            RequestReceivedDate = DateTime.Now,
            AuditCreatedByUserId = context.GetLoggedUserId(),
            AuditUpdatedByUserId = context.GetLoggedUserId(),
            HttpMethod = context.Request.Method,
            HttpRequestPath = context.Request.Path.ToString(),
            HttpQueryString = context.Request.QueryString.ToString(),
            HttpScheme = context.Request.Scheme,
            HttpProtocol = context.Request.Protocol,
            HttpRequestHeadersJson = JsonConvert.SerializeObject(context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), Formatting.Indented),
            ConnectionId = context.Connection.Id,
            ClientRemoteIpAddress = context.GetRemoteIpAddress()?.ToString(),
            ClientRemotePort = context.Connection.RemotePort.ToString(),
            ServerLocalIpAddress = context.Connection.LocalIpAddress?.ToString(),
            ServerLocalPort = context.Connection.LocalPort.ToString(),
            RequestHost = context.Request.Host.Value,
            ServerMachineName = Environment.MachineName,
            ApplicationName = AppDomain.CurrentDomain.FriendlyName,
            IsAuthenticated = context.User.Identity?.IsAuthenticated,
            AuthenticatedUserName = context.User.Identity?.Name,
            AuthenticatedUserRoles = string.Join(",", context.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value)),
            UserAgent = context.Request.Headers["User-Agent"].ToString(),
            Referrer = context.Request.Headers["Referer"].ToString()
        };

        if (context.Request.ContentLength > 0)
        {
            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            log.HttpRequestBody = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
        }
        else
        {
            log.HttpRequestBody = string.Empty;
        }

        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            log.HasException = true;
            log.ExceptionType = ex.GetType().FullName;
            log.ExceptionMessage = ex.Message;
            log.ExceptionStackTrace = ex.StackTrace;
            log.HttpResponseStatusCode = StatusCodes.Status500InternalServerError;
            log.HttpResponseIsSuccess = false;

            throw;
        }
        finally
        {
            stopwatch.Stop();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await responseBodyStream.CopyToAsync(originalResponseBody);
            context.Response.Body = originalResponseBody;

            log.ResponseSendDate = DateTime.Now;
            log.RequestDurationMilliseconds = stopwatch.ElapsedMilliseconds;
            log.HttpResponseStatusCode ??= context.Response.StatusCode;
            log.HttpResponseIsSuccess ??= context.Response.StatusCode is >= 200 and < 300;
            log.HttpResponseContentType = context.Response.ContentType;
            log.HttpResponseContentLength = Encoding.UTF8.GetByteCount(responseBodyText);
            log.HttpResponseBodySummary = responseBodyText;

            try
            {
                TrafficAuditLogger.Log(log);
            }
            catch (Exception auditEx)
            {
                ErrorLogger.LogError($"[{nameof(TrafficAuditMiddleware)}] Audit write failed", auditEx);
            }
        }
    }
}