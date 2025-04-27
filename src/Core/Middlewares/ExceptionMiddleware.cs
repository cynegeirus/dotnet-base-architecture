using System.Net;
using System.Reflection;
using Core.Entities.Concrete.Log;
using Core.Utilities.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        FileLogHelper.WriteLog(new ErrorLog
        {
            Logged = DateTime.Now,
            Title = MethodBase.GetCurrentMethod()?.Name,
            Exception = ex
        });

        return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
        {
            Code = httpContext.Response.StatusCode,
            Message = "Internal Server Error"
        }, Formatting.Indented));
    }
}