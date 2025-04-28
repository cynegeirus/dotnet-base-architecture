using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.Identity;

[Table("Menu", Schema = "management")]
public class Menu : BaseEntity
{
    public int? Priority { get; set; }
    public string? Title { get; set; }
    public string? ControllerName { get; set; }
    public string? ActionName { get; set; }
    public string? Icon { get; set; }
}