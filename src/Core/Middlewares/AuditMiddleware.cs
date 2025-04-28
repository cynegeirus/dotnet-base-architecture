using System.Reflection;
using Core.Entities.Concrete.Log;
using Core.Extensions;
using Core.Utilities.Helpers;
using Microsoft.AspNetCore.Http;

namespace Core.Middlewares;

public class AuditMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        TransactionLog? log = new()
        {
            Logged = DateTime.Now,
            ConnectionId = context.Connection.Id,
            RemoteIp = context.GetRemoteIPAddress().ToString(),
            RemotePort = context.Connection.RemotePort.ToString(),
            LocalIp = context.Connection.LocalIpAddress.ToString(),
            LocalPort = context.Connection.LocalPort.ToString(),
            Path = context.Request.Path,
            Method = context.Request.Method,
            QueryString = context.Request.QueryString.ToString()
        };

        if (context.Request.Method == "POST")
        {
            context.Request.EnableBuffering();
            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;
            log.Payload = body;
        }

        if (context.Request.Method == "PUT")
        {
            context.Request.EnableBuffering();
            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;
            log.Payload = body;
        }

        if (context.Request.Method == "DELETE")
        {
            context.Request.EnableBuffering();
            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;
            log.Payload = body;
        }

        log.RequestedOn = DateTime.Now;

        await using (var originalRequest = context.Response.Body)
        {
            try
            {
                using MemoryStream? memStream = new();
                context.Response.Body = memStream;
                memStream.Position = 0;
                var response = await new StreamReader(memStream).ReadToEndAsync();
                log.Response = response;
                log.ResponseCode = context.Response.StatusCode.ToString();
                log.IsSuccess = context.Response.StatusCode is 200 or 201;
                log.RespondedOn = DateTime.Now;

                if (context?.Request?.Host.HasValue ?? false)
                    FileLogHelper.WriteLog(log);

                memStream.Position = 0;
                await memStream.CopyToAsync(originalRequest);
            }
            catch (Exception ex)
            {
                FileLogHelper.WriteLog(new ErrorLog
                {
                    Logged = DateTime.Now,
                    Title = MethodBase.GetCurrentMethod()?.Name,
                    Exception = ex
                });
            }
            finally
            {
                if (context != null)
                    context.Response.Body = originalRequest;
            }
        }

        await next.Invoke(context);
    }
}