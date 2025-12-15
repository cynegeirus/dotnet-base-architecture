using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;

namespace Core.Aspects.Autofac.Logging;

public class LogAspect : MethodInterception
{
    private readonly LoggerServiceBase? _loggerServiceBase;

    public LogAspect(Type loggerService)
    {
        if (loggerService.BaseType != typeof(LoggerServiceBase)) throw new System.Exception(AspectMessages.WrongLoggerType);

        _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService)!;
    }

    protected override void OnBefore(IInvocation invocation)
    {
        _loggerServiceBase?.Info(GetLogDetail(invocation));
    }

    private LogDetail GetLogDetail(IInvocation invocation)
    {
        var logParameters = invocation.Arguments.Select((t, i) => new LogParameter { Name = invocation.GetConcreteMethod().GetParameters()[i].Name, Value = t, Type = t.GetType().Name }).ToList();

        LogDetail logDetail = new()
        {
            MethodName = invocation.Method.Name,
            LogParameters = logParameters
        };

        return logDetail;
    }
}