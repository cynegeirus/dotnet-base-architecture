using Core.Utilities.Helpers;

namespace Core.CrossCuttingConcerns.Caching;

public class CacheOverview
{
    public string? IpAddress { get; set; } = NetworkHelper.GetLocalIpAddress();
    public string? MachineName { get; set; } = Environment.MachineName;
    public string? DomainName { get; set; } = Environment.UserDomainName;
    public string? UserName { get; set; } = Environment.UserName;
    public int? Duration { get; set; }
    public int? Count { get; set; }
    public List<string>? Keys { get; set; }
}