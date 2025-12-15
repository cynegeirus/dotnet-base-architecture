using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Concrete.Base;
using Entities.Enumerations;

namespace Entities.Concrete;

[Table(nameof(Notification), Schema = "main")]
public class Notification : BaseEntity
{
    public NotificationType Type { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
}