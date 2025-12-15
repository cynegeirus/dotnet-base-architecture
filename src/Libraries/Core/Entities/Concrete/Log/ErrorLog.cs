using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.Log;

[Table("ErrorLog", Schema = "audit")]
public class ErrorLog : BaseEntity
{
    public DateTime LoggedAt { get; set; }
    public string? Level { get; set; }
    public string? Source { get; set; }
    public string? Message { get; set; }
    public string? ExceptionType { get; set; }
    public string? ExceptionMessage { get; set; }
    public string? StackTrace { get; set; }
    public string? InnerException { get; set; }
}