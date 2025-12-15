using log4net.Core;

namespace Core.CrossCuttingConcerns.Logging.Log4Net;

[Serializable]
public class SerializableLogEvent(LoggingEvent loggingEvent)
{
    public DateTime Timestamp => loggingEvent.TimeStamp;
    public string? Level => loggingEvent.Level?.Name;
    public string? Logger => loggingEvent.LoggerName;
    public object? Message => loggingEvent.MessageObject;

    public ExceptionInfo? Exception => loggingEvent.ExceptionObject != null
        ? new ExceptionInfo(loggingEvent.ExceptionObject)
        : null;
}

[Serializable]
public class ExceptionInfo(Exception exception)
{
    public string? Type => exception.GetType().FullName;
    public string? Message => exception.Message;
    public string? StackTrace => exception.StackTrace;
    public string? InnerException => exception.InnerException?.Message;
}