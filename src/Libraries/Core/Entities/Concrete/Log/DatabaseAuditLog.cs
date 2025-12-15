using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.Log;

[Table("DatabaseAuditLog", Schema = "audit")]
public class DatabaseAuditLog : BaseEntity
{
    public string? TableName { get; set; }
    public string? EntityName { get; set; }
    public string? PrimaryKey { get; set; }
    public string? Operation { get; set; }
    public string? Json { get; set; }
}