using Core.Dtos.Concrete;

namespace Entities.Dtos.Responses;

public class RoleResponseDto : BaseDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}