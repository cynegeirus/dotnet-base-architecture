using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching;

public class CacheActionFilterAttribute : Attribute, IAsyncActionFilter
{
    private readonly int _duration = ConfigurationHelper.GetConfigWithFile("configurationSettings.json").GetValue<int>("Cache:Duration");

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var cacheManager = context.HttpContext.RequestServices.GetService<ICacheManager>();
        if (cacheManager == null)
        {
            await next();
            return;
        }

        var methodName = $"{context.ActionDescriptor.DisplayName}";
        var arguments = context.ActionArguments.Values.ToList();
        var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";

        if (cacheManager.IsAdd(key))
        {
            var cachedValue = cacheManager.Get<object>(key);
            context.Result = new OkObjectResult(cachedValue);
            return;
        }

        var executedContext = await next();
        if (executedContext.Result is ObjectResult objectResult) cacheManager.Add(key, objectResult.Value!, _duration);
    }
}