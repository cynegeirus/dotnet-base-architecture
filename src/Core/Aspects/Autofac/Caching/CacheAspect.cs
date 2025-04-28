using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Helpers;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching;

public class CacheAspect : MethodInterception
{
    private readonly ICacheManager? _cacheManager = ServiceTool.ServiceProvider!.GetService<ICacheManager>();
    private readonly int _duration = ConfigurationHelper.GetConfigWithFile("configurationSettings.json").GetValue<int>("Cache:Duration");

    public override void Intercept(IInvocation invocation)
    {
        var methodName = string.Format($"{invocation.Method.ReflectedType?.FullName}.{invocation.Method.Name}");
        var arguments = invocation.Arguments.ToList();
        var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
        if (_cacheManager != null && _cacheManager.IsAdd(key))
        {
            invocation.ReturnValue = _cacheManager.Get(key);
            return;
        }

        invocation.Proceed();
        _cacheManager?.Add(key, invocation.ReturnValue, _duration);
    }
}