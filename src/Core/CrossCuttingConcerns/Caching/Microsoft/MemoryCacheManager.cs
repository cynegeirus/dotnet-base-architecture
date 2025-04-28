using System.Reflection;
using Core.Utilities.Helpers;
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns.Caching.Microsoft;

public class MemoryCacheManager : ICacheManager
{
    private readonly int _duration = ConfigurationHelper.GetConfigWithFile("configurationSettings.json").GetValue<int>("Cache:Duration");
    private readonly IMemoryCache? _memoryCache = ServiceTool.ServiceProvider!.GetService<IMemoryCache>();

    public CacheOverview Overview()
    {
        return new CacheOverview
        {
            Duration = _duration,
            Count = GetAllKeys().Count,
            Keys = GetAllKeys()
        };
    }

    public List<string> GetAllKeys()
    {
        return _memoryCache is not MemoryCache memoryCache ? [] : memoryCache.Keys.Select(cacheKey => cacheKey.ToString()!).ToList();
    }

    public T? Get<T>(string key)
    {
        return _memoryCache!.Get<T>(key);
    }

    public object? Get(string key)
    {
        return _memoryCache!.Get(key);
    }

    public void Add(string key, object data, int duration)
    {
        _memoryCache!.Set(key, data, TimeSpan.FromMinutes(duration));
    }

    public bool IsAdd(string key)
    {
        return _memoryCache!.TryGetValue(key, out _);
    }

    public void Remove(string key)
    {
        _memoryCache!.Remove(key);
    }

    public void RemoveAll()
    {
        foreach (var key in GetAllKeys()!) _memoryCache?.Remove(key);
    }
}