using Core.Dtos.Concrete;

namespace Entities.Dtos.Responses;

public class MenuResponseDto : BaseDto
{
    public int? Priority { get; set; }
    public string? Title { get; set; }
    public string? ControllerName { get; set; }
    public string? ActionName { get; set; }
    public string? Icon { get; set; }
}