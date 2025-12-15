namespace Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

public class FileLogger : LoggerServiceBase
{
    private static readonly Lazy<FileLogger> _instance = new(() => new FileLogger());
    public static FileLogger Instance => _instance.Value;

    public FileLogger() : base("JsonFileLogger")
    {
    }
}