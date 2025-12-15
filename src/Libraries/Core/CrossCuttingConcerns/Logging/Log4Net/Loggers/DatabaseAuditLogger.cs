using Core.Entities.Concrete.Configuration;
using Core.Entities.Concrete.Log;
using Core.Utilities.Helpers;
using Microsoft.Extensions.Configuration;

namespace Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

public class DatabaseAuditLogger : LoggerServiceBase
{
    private static readonly Lazy<DatabaseAuditLogger> _instance = new(() => new DatabaseAuditLogger());

    private static readonly Lazy<LogTargetConfiguration> _config = new(() =>
        ConfigurationHelper.GetConfig().GetSection("Logging:DatabaseAudit").Get<LogTargetConfiguration>()
        ?? new LogTargetConfiguration { WriteToDatabase = true, WriteToFile = true });

    public static DatabaseAuditLogger Instance => _instance.Value;
    public static LogTargetConfiguration Config => _config.Value;

    public static bool ShouldWriteToDatabase => Config.WriteToDatabase;

    private DatabaseAuditLogger() : base("DatabaseAuditLogger")
    {
    }

    public static void Log(DatabaseAuditLog auditLog)
    {
        if (Config.WriteToFile) Instance.Info(auditLog);
    }
}