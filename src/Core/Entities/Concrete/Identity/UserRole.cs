using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.Identity;

[Table("UserRole", Schema = "management")]
public class UserRole : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

    public virtual User? User { get; set; }
    public virtual Role? Role { get; set; }
}