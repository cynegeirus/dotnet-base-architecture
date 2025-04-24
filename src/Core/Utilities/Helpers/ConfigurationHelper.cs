using Microsoft.Extensions.Configuration;

namespace Core.Utilities.Helpers;

public class ConfigurationHelper
{
    private static IConfigurationRoot _configuration;

    static ConfigurationHelper()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("configurationSettings.json", true, true)
            .AddEnvironmentVariables()
            .Build();
    }

    public static IConfigurationRoot GetConfig()
    {
        return _configuration;
    }

    public static string? GetSetting(string key)
    {
        return _configuration[key];
    }

    public static IConfigurationRoot GetConfigWithFile(string fileName)
    {
        return _configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(fileName, true, true)
            .AddEnvironmentVariables()
            .Build();
    }
}