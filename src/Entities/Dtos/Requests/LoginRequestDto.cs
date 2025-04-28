using Core.Dtos.Concrete;

namespace Entities.Dtos.Requests;

public class LoginRequestDto : BaseDto
{
    public required string? Username { get; set; }
    public required string? Password { get; set; }
}