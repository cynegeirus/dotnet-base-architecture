namespace Core.CrossCuttingConcerns.Caching;

public interface ICacheManager
{
    CacheOverview Overview();
    List<string>? GetAllKeys();
    T? Get<T>(string key);
    object? Get(string key);
    void Add(string key, object data, int duration);
    bool IsAdd(string key);
    void Remove(string key);
    void RemoveAll();
}