using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.Management;

[Table("SystemParameter", Schema = "management")]
public class SystemParameter : BaseEntity
{
    public string? Name { get; set; }
    public string? Value { get; set; }
}