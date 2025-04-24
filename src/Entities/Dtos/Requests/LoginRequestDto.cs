using Core.Entities.Concrete.Base;

namespace Entities.Dtos.Requests;

public class LoginRequestDto : BaseDto
{
    public required string? Username { get; set; }
    public required string? Password { get; set; }
}