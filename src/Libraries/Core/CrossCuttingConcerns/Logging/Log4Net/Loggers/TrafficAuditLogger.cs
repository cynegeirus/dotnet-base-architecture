using Core.DataAccess.EntityFramework.Contexts;
using Core.Entities.Concrete.Configuration;
using Core.Entities.Concrete.Log;
using Core.Utilities.Helpers;
using Microsoft.Extensions.Configuration;

namespace Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

public class TrafficAuditLogger : LoggerServiceBase
{
    private static readonly Lazy<TrafficAuditLogger> _instance = new(() => new TrafficAuditLogger());

    private static readonly Lazy<LogTargetConfiguration> _config = new(() =>
        ConfigurationHelper.GetConfig().GetSection("Logging:TrafficAudit").Get<LogTargetConfiguration>()
        ?? new LogTargetConfiguration { WriteToDatabase = true, WriteToFile = true });

    public static TrafficAuditLogger Instance => _instance.Value;
    private static LogTargetConfiguration Config => _config.Value;

    private TrafficAuditLogger() : base("TrafficAuditLogger")
    {
    }

    public static void Log(TrafficAuditLog auditLog)
    {
        if (Config.WriteToFile) Instance.Info(auditLog);

        if (Config.WriteToDatabase) WriteToDatabase(auditLog);
    }

    private static void WriteToDatabase(TrafficAuditLog auditLog)
    {
        try
        {
            using var context = new BaseDbContext();
            context.TrafficAuditLog.Add(auditLog);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            ErrorLogger.LogError($"[{nameof(TrafficAuditLogger)}] Failed to write to database", ex);
        }
    }
}