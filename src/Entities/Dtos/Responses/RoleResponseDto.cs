using Core.Entities.Concrete.Base;

namespace Entities.Dtos.Responses;

public class RoleResponseDto : BaseDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}