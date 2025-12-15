using Core.DataAccess.EntityFramework.Contexts;
using Core.Entities.Concrete.Configuration;
using Core.Entities.Concrete.Log;
using Core.Utilities.Helpers;
using Microsoft.Extensions.Configuration;

namespace Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

public class ErrorLogger : LoggerServiceBase
{
    private static readonly Lazy<ErrorLogger> _instance = new(() => new ErrorLogger());
    private static readonly Lazy<LogTargetConfiguration> _config = new(() => ConfigurationHelper.GetConfig().GetSection("Logging:Error").Get<LogTargetConfiguration>() ?? new LogTargetConfiguration { WriteToDatabase = true, WriteToFile = true });

    public static ErrorLogger Instance => _instance.Value;
    private static LogTargetConfiguration Config => _config.Value;

    private ErrorLogger() : base("ErrorLogger")
    {
    }

    public static void LogError(string message, Exception? exception = null)
    {
        var errorLog = CreateErrorLog(message, exception);
        if (Config.WriteToFile)
        {
            if (exception != null) Instance.Error(new { Message = message }, exception);
            else Instance.Error(new { Message = message });
        }

        if (Config.WriteToDatabase) WriteToDatabase(errorLog);
    }

    public static void LogError(Exception exception)
    {
        LogError(exception.Message, exception);
    }

    private static ErrorLog CreateErrorLog(string message, Exception? exception)
    {
        return new ErrorLog
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now,
            IsUpdated = false,
            IsDeleted = false,
            LoggedAt = DateTime.Now,
            Level = "ERROR",
            Source = exception?.Source,
            Message = message,
            ExceptionType = exception?.GetType().FullName,
            ExceptionMessage = exception?.Message,
            StackTrace = exception?.StackTrace,
            InnerException = exception?.InnerException?.Message
        };
    }

    private static void WriteToDatabase(ErrorLog errorLog)
    {
        try
        {
            using var context = new BaseDbContext();
            context.ErrorLog.Add(errorLog);
            context.SaveChanges();
        }
        catch
        {
            /* DB yazma hatas? olursa sessizce geç - sonsuz döngü önlenir */
        }
    }
}