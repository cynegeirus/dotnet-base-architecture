namespace Core.Entities.Concrete.Configuration;

public class LoggingConfiguration
{
    public LogTargetConfiguration Error { get; set; } = new();
    public LogTargetConfiguration TrafficAudit { get; set; } = new();
    public LogTargetConfiguration DatabaseAudit { get; set; } = new();
}

public class LogTargetConfiguration
{
    public bool WriteToDatabase { get; set; }
    public bool WriteToFile { get; set; }
}