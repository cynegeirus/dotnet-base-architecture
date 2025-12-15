using Core.Dtos.Concrete.Base;

namespace Entities.Dtos;

public class LoginDto : BaseDto
{
    public required string? Username { get; set; }
    public required string? Password { get; set; }
}