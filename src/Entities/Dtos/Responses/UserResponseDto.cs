using Core.Dtos.Concrete;

namespace Entities.Dtos.Responses;

public class UserResponseDto : BaseDto
{
    public required string? FirstName { get; set; }
    public required string? LastName { get; set; }
    public required string? MailAddress { get; set; }
    public required string? Username { get; set; }

    public RoleResponseDto? Role { get; set; }
    public MenuResponseDto? Menu { get; set; }
}