using System.Reflection;
using System.Xml;
using log4net;
using log4net.Config;
using log4net.Repository;
using log4net.Repository.Hierarchy;

namespace Core.CrossCuttingConcerns.Logging.Log4Net;

public class LoggerServiceBase
{
    private readonly ILog _log;
    private static readonly object _lock = new();
    private static ILoggerRepository? _loggerRepository;

    public bool IsInfoEnabled => _log.IsInfoEnabled;
    public bool IsDebugEnabled => _log.IsDebugEnabled;
    public bool IsWarnEnabled => _log.IsWarnEnabled;
    public bool IsFatalEnabled => _log.IsFatalEnabled;
    public bool IsErrorEnabled => _log.IsErrorEnabled;

    public LoggerServiceBase(string name)
    {
        EnsureRepositoryConfigured();
        _log = LogManager.GetLogger(_loggerRepository!.Name, name);
    }

    private static void EnsureRepositoryConfigured()
    {
        if (_loggerRepository != null) return;

        lock (_lock)
        {
            if (_loggerRepository != null) return;

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(File.OpenRead("log4net.config"));

            _loggerRepository = LogManager.CreateRepository(
                Assembly.GetEntryAssembly()!,
                typeof(Hierarchy));

            XmlConfigurator.Configure(_loggerRepository, xmlDocument["log4net"]!);
        }
    }

    public void Info(object logMessage)
    {
        if (IsInfoEnabled) _log.Info(logMessage);
    }

    public void Debug(object logMessage)
    {
        if (IsDebugEnabled) _log.Debug(logMessage);
    }

    public void Warn(object logMessage)
    {
        if (IsWarnEnabled) _log.Warn(logMessage);
    }

    public void Fatal(object logMessage)
    {
        if (IsFatalEnabled) _log.Fatal(logMessage);
    }

    public void Error(object logMessage)
    {
        if (IsErrorEnabled) _log.Error(logMessage);
    }

    public void Error(object logMessage, Exception exception)
    {
        if (IsErrorEnabled) _log.Error(logMessage, exception);
    }
}