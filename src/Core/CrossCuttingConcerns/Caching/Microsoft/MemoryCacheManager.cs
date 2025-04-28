using System.Reflection;
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns.Caching.Microsoft;

public class MemoryCacheManager : ICacheManager
{
    private readonly IMemoryCache? _cache = ServiceTool.ServiceProvider!.GetService<IMemoryCache>();

    public CacheOverview Overview()
    {
        return new CacheOverview { Count = GetAllKeys()?.Count, Keys = GetAllKeys() };
    }

    public List<string> GetAllKeys()
    {
        return _cache is not MemoryCache memoryCache ? [] : memoryCache.Keys.Select(cacheKey => cacheKey.ToString()!).ToList();
    }

    public T? Get<T>(string key)
    {
        return _cache!.Get<T>(key);
    }

    public object? Get(string key)
    {
        return _cache!.Get(key);
    }

    public void Add(string key, object data, int duration)
    {
        _cache!.Set(key, data, TimeSpan.FromMinutes(duration));
    }

    public bool IsAdd(string key)
    {
        return _cache!.TryGetValue(key, out _);
    }

    public void Remove(string key)
    {
        _cache!.Remove(key);
    }

    public void RemoveAll()
    {
        foreach (var key in GetAllKeys()!) _cache?.Remove(key);
    }
}