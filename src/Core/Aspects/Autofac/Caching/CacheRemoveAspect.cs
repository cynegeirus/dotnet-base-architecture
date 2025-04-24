using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching;

public class CacheRemoveAspect(string pattern) : MethodInterception
{
    private readonly ICacheManager? _cacheManager = ServiceTool.ServiceProvider!.GetService<ICacheManager>();

    protected override void OnSuccess(IInvocation invocation)
    {
        _cacheManager?.RemoveByPattern(pattern);
    }
}