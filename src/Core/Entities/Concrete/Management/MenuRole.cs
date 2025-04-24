using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.Management;

[Table("MenuRole", Schema = "management")]
public class MenuRole : BaseEntity
{
    public Guid? RoleId { get; set; }
    public Guid? MenuId { get; set; }

    public virtual Role? Role { get; set; }
    public virtual Menu? Menu { get; set; }
}