using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.Management;

[Table("Notification", Schema = "management")]
public class Notification : BaseEntity
{
    public string? Title { get; set; }
    public string? Content { get; set; }
}