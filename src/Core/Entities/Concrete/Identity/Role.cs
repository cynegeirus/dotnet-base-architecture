using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.Identity;

[Table("Role", Schema = "management")]
public class Role : BaseEntity
{
    public string? Name { get; set; }
}